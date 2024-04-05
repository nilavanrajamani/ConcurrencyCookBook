using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class LocationUnknownException : Exception
    {
        internal LocationUnknownException()
        { }
    }
    public struct Location
    {
        double latitude;
        double longitude;

        public Location(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
        public double Latitude
        {
            get { return latitude; }
        }
        public double Longitude
        {
            get { return longitude; }
        }
    }

    public class LocationReporter : IObserver<Location>
    {
        private string instName;
        public LocationReporter(string name)
        {
            this.instName = name;
        }
        public string Name
        { get { return this.instName; } }
        public void OnCompleted()
        {
            Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("{0}: The location cannot be determined.", this.Name);
        }

        public void OnNext(Location value)
        {
            Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
        }
    }

    public class LocationTracker : IObservable<Location>
    {
        private List<IObserver<Location>> observers;
        public LocationTracker()
        {
            observers = new List<IObserver<Location>>();
        }

        public IDisposable Subscribe(IObserver<Location> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        public void TrackLocation(Nullable<Location> loc)
        {
            foreach (var observer in observers)
            {
                if (!loc.HasValue)
                    observer.OnError(new LocationUnknownException());
                else
                    observer.OnNext(loc.Value);
            }
        }
    }

    public class Unsubscriber : IDisposable
    {
        private List<IObserver<Location>> _observers;
        private IObserver<Location> _observer;

        public Unsubscriber(List<IObserver<Location>> observers, IObserver<Location> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
    public class Observer
    {
        static async Task Main(string[] args)
        {
            LocationTracker provider = new LocationTracker();
            LocationReporter reporter1 = new LocationReporter("FixedGPS");
            IDisposable GPSUnsubscriber = provider.Subscribe(reporter1);

            LocationReporter reporter2 = new LocationReporter("MobileGPS");
            IDisposable MobileUnSubscriber = provider.Subscribe(reporter2);

            provider.TrackLocation(new Location(47.6456, -122.1312));
            GPSUnsubscriber.Dispose();
            provider.TrackLocation(new Location(47.6677, -122.1199));
            provider.TrackLocation(null);            
        }
    }
}
