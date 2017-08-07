using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2J
{
    public class Model
    {
        public string __comment;
        public Dictionary<string, string> textures;
        public Element[] elements;

        public class Element
        {
            public float[] from;
            public float[] to;
            public Dictionary<string, Face> faces;

            public class Face
            {
                public string texture;
                public float[] uv;
            }
        }
    }
}
