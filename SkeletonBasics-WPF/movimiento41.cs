using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    class movimiento41
    {
        public struct ptMov
        {
            public float X;
            public float Y;
            public float Z;
        };

        public enum posturas
        {
            MInicial,
            MCorrecto,
            MPasado,
            MCorto,
            Error
        };

        ptMov centerHip, rightHip, leftHip, kneeLeft, kneeRight, ankleLeft, ankleRight;

        public void Movimiento41()
        {

            this.centerHip = new ptMov();
            this.kneeLeft = new ptMov();
            this.kneeRight = new ptMov();
            this.ankleLeft = new ptMov();
            this.ankleRight = new ptMov();
            this.rightHip = new ptMov();
            this.leftHip = new ptMov();

        }

        //Obtengo las coordenadas de los puntos.
        public void damePuntos(Skeleton skel)
        {
            //Obtengos puntos de la cadera. Punto central
            centerHip.X = skel.Joints[JointType.HipCenter].Position.X;
            centerHip.Y = skel.Joints[JointType.HipCenter].Position.Y;
            centerHip.Z = skel.Joints[JointType.HipCenter].Position.Z;
            //Obtengos puntos de la cadera derecha.
            rightHip.X = skel.Joints[JointType.HipRight].Position.X;
            rightHip.Y = skel.Joints[JointType.HipRight].Position.Y;
            rightHip.Z = skel.Joints[JointType.HipRight].Position.Z;
            //Obtengos puntos de la cadera izquierda.
            leftHip.X = skel.Joints[JointType.HipLeft].Position.X;
            leftHip.Y = skel.Joints[JointType.HipLeft].Position.Y;
            leftHip.Z = skel.Joints[JointType.HipLeft].Position.Z;
            //Puntos de la rodilla izquierda.
            kneeLeft.X = skel.Joints[JointType.KneeLeft].Position.X;
            kneeLeft.Y = skel.Joints[JointType.KneeLeft].Position.Y;
            kneeLeft.Z = skel.Joints[JointType.KneeLeft].Position.Z;
            //Puntos de la rodilla derecha.
            kneeRight.X = skel.Joints[JointType.KneeRight].Position.X;
            kneeRight.Y = skel.Joints[JointType.KneeRight].Position.Y;
            kneeRight.Z = skel.Joints[JointType.KneeRight].Position.Z;
            //Puntos del tobillo izquierdo.
            ankleLeft.X = skel.Joints[JointType.AnkleLeft].Position.X;
            ankleLeft.Y = skel.Joints[JointType.AnkleLeft].Position.Y;
            ankleLeft.Z = skel.Joints[JointType.AnkleLeft].Position.Z;
            //Puntos del tobillo derecho.
            ankleRight.X = skel.Joints[JointType.AnkleRight].Position.X;
            ankleRight.Y = skel.Joints[JointType.AnkleRight].Position.Y;
            ankleRight.Z = skel.Joints[JointType.AnkleRight].Position.Z;

        }

        //Comprueba en que posicion se encuentra el esquelo y devuelve un valor.
        public int detection(Skeleton skel, float valor, float error)
        {
            damePuntos(skel);

            if (comprueba_correcta(valor, error))
            {
                return (int)posturas.MCorrecto;
            }
            else if (comprueba_inicial(error))
            {
                return (int)posturas.MInicial;
            }
            else if (comprueba_pasado(valor, error))
            {
                return (int)posturas.MPasado;
            }
            else 
            {
                return (int)posturas.MCorto;
            }

            return (int)posturas.Error;
        }
        //Comprueba si el esqueleto esta en la posicion inicial
        public bool comprueba_inicial(float error)
        {
            if ((kneeLeft.Z - ankleLeft.Z) - (centerHip.Z - kneeLeft.Z) > error
                && (kneeRight.Z - ankleRight.Z) - (centerHip.Z - kneeRight.Z) > error)
            {
                return true;
            }
            return false;
        }
        //Comprueba si el esqueleto esta en la posicion correcta
        public bool comprueba_correcta(float valor, float error)
        {
            if ((centerHip.Z + valor - kneeRight.Z) < error && (centerHip.Z + valor - kneeLeft.Z) < error)
            {
                return true;
            }

            return false;
        }
        //Comprueba si el esqueleto esta en la posicion pasada
        public bool comprueba_pasado(float valor, float error)
        {

            if ((centerHip.Z + valor - kneeRight.Z) > error && (centerHip.Z + valor - kneeLeft.Z) > error)
            {
                return true;
            }

            return false;
        }
    }
}
