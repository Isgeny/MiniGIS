using System.Windows.Forms;

namespace MiniGIS
{
    public enum MapObjectType
    {
        Point,
        Line,
        PolyLine,
        Polygon
    }

    public abstract class MapObject
    {
        public MapObjectType Type { get; set; }

        // Слой, в котором находится данный объект
        public Layer Layer {get; set; }

        // Прямоугольная область, в которую вписан данный объект
        public GEORect GEOBounds
        { 
            get
            {
                return GetBounds();
            }
        }

        // Выделен ли объект (рисуется пунктирный контур)
        public bool Selected { get; set; }

        // Рисование объекта
        public abstract void Draw(PaintEventArgs e);

        // Получение прямоугольной области
        public abstract GEORect GetBounds();

        // Проверка вхождения прямоугольника в прямоугольник текущего объекта
        public abstract bool IsInside(GEORect geoRect);
    }
}