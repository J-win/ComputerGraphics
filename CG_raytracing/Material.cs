using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_raytracing
{
    class Material
    {
        public Vector3 Color;
        public Vector4 LightCoeffs;
        public float ReflectionCoef;
        public float RefractionCoef;
        public int MaterialType;
        public Material(Vector3 color, Vector4 lightCoefs, float reflectionCoef, float refractionCoef, int type)
        {
            Color = color;
            LightCoeffs = lightCoefs;
            ReflectionCoef = reflectionCoef;
            RefractionCoef = refractionCoef;
            MaterialType = type;
        }
    }
}
