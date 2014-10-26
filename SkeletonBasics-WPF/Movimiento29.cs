using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    class Movimiento29
    {
        private float precisionAdmitida;
        private float desplazamientoEje;


        public Movimiento29(float precision, float desplazamiento)
        {
            this.precisionAdmitida = precision;
            this.desplazamientoEje = desplazamiento;
        }

        private float getDistance(float[] coordenadasPierna1, float[] coordenadasPierna2) {
            float distancia = coordenadasPierna1[0] - coordenadasPierna2[0];
            distancia += coordenadasPierna1[1] - coordenadasPierna2[1];
            return distancia;
        }

        private bool comprobarValoresIntervalo(float valor, float precision, float valorEstablecido) {
            float distancia = valor - valor*(precision/100);
            if (Math.Abs(distancia) <= valorEstablecido) {
                return true;
            }
            return false;
        }


        /**
         * Comprueba si se ha producido un desplazamiento correcto del pie derecho sobre el eje -X
         * @params skeleton Esqueleto a analizar
         * 
         * @return 
         *      · 0 -> El desplazamiento ha sido correcto
         *      · 1 -> El desplazamiento ha excedido la distancia establecida con el error definido por la variable precisionAdmitida
         *      · 2 -> El desplazamiento ha sido inferior al de la distancia establecida con el error máximo definido por la variable precisionAdmitida
         *      · 3 -> La pierna no está en una posición correcta.
         * 
         * */
        public byte isPiernaDesplazadaEjeNegZ(Skeleton skeleton)
        {

            float[] coordenadasPiernaIzquierda = new float[2];
            float[] coordenadasPiernaDerecha = new float[2];

            if ((skeleton.Joints[Microsoft.Kinect.JointType.FootRight].TrackingState == JointTrackingState.Tracked) && (skeleton.Joints[Microsoft.Kinect.JointType.FootLeft].TrackingState == JointTrackingState.Tracked))
            {
                coordenadasPiernaDerecha[0] = skeleton.Joints[Microsoft.Kinect.JointType.FootRight].Position.Z;
                coordenadasPiernaDerecha[1] = skeleton.Joints[Microsoft.Kinect.JointType.FootRight].Position.Y;

                coordenadasPiernaIzquierda[0] = skeleton.Joints[Microsoft.Kinect.JointType.FootLeft].Position.Z;
                coordenadasPiernaIzquierda[1] = skeleton.Joints[Microsoft.Kinect.JointType.FootLeft].Position.Y;

                float distancia = getDistance(coordenadasPiernaDerecha, coordenadasPiernaIzquierda);

                if (coordenadasPiernaDerecha[0] < coordenadasPiernaIzquierda[0])
                {


                    if (comprobarValoresIntervalo(distancia, this.precisionAdmitida, this.desplazamientoEje))
                    {
                        return 0; //La pierna está en su sitio final

                    }
                   
                    else if (distancia > --this.desplazamientoEje)
                    {
                        return 1; // La pierna aún no ha llegado a su sitio
                    }
                    else
                    {
                        return 2; // La pierna se ha pasado
                    }
                    
                }
            }
            return 3;
            
        }
    }
}
