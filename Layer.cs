using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public class Layer
    {
        public List<MapObject> MapObjects { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public Map Map { get; set; }

        public Layer()
        {
            Name = "";
            Visible = true;
            MapObjects = new List<MapObject>();
        }

        public void AddMapObject(MapObject mapObject)
        {
            mapObject.Layer = this;
            MapObjects.Add(mapObject);
        }

        public void InsertMapObject(int index, MapObject mapObject)
        {
            MapObjects.Insert(index, mapObject);
        }

        public void RemoveMapObject(MapObject mapObject)
        {
            MapObjects.Remove(mapObject);
        }

        public int CountMapObjects()
        {
            return MapObjects.Count;
        }
    }
}