using System.Collections.Generic;
using UnityEngine;

namespace Main {
    public class FaceGenerator : MonoBehaviour
    {
        private const int QuantityFaceCub = 4;
        private const int QuantityFaceLine = 3;

        public List<Face> CreateClassic()
        {
            List<Face> faces = new List<Face>();

            faces.Add(new Face());
            faces[0].Init();

            return faces;
        }

        public List<Face> CreateLine(int quantity = QuantityFaceLine)
        {
            List<Face> faces = CreateClassic();

            for (int i = 1; i < quantity - 1; i++)
            {
                faces.Add(new Face());
                faces[i].Init(faces[i - 1]);
            }

            faces.Add(new Face());
            faces[faces.Count - 1].Init(faces[faces.Count - 2], faces[0]);

            return faces;
        }

        public List<Face> CreateCub(out Face upFace, out Face downFaceUp)
        {
            List<Face> faces = CreateLine(QuantityFaceCub);

            upFace = new Face();
            upFace.InitUp(faces);

            downFaceUp = new Face();
            downFaceUp.InitDown(faces);

            return faces;
        }
    }
}
