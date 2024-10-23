using Loop;
using Main;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace Mover {
    public class FaceMover
    {
        private const int IndexActivFaceCub = 0;
        private const int IndexRightFaceCub = 1;
        private const int IndexRearFaceCub = 2;
        private const int IndexLeftFaceCub = 3;

        private Face _currentFace;
        private Face[] _faces;

        public void Move(MoveType type, List<Face> faces, ref Face faceUp, ref Face faceDown)
        {
            switch (type)
            {
                case MoveType.Left:
                    MoveLeft(faces, faceUp, faceDown);
                    break; 

                case MoveType.Right:
                    MoveRight(faces, faceUp, faceDown);
                    break; 

                case MoveType.Up:
                    MoveUp(faces, ref faceUp, ref faceDown);
                    break;
                    
                case MoveType.Down:
                    MoveDown(faces, ref faceUp, ref faceDown);
                    break;
            }
        }

        private void MoveLeft(List<Face> faces, Face faceUp, Face faceDown)
        {
            MoveLine(
                faces,
                faceUp,
                faceDown, 
                1,
                0, 
                (i) => i + 1);
        }

        private void MoveRight(List<Face> faces, Face faceUp, Face faceDown)
        {
            MoveLine(
                faces, 
                faceDown, 
                faceUp, 
                faces.Count - 1,
                faces.Count - 2, 
                (i) => i - 1);
        }

        private void MoveLine(
            List<Face> faces,
            Face turnRight,
            Face turnLeft, 
            int indexNewFirstValue,
            int indexNewLastValue,
            Func<int, int> indexOffset)
        {
            _faces = faces.ToArray();

            for (int i = 1; i < faces.Count - 1; i++)
            {
                faces[i] = _faces[indexOffset(i)];
            }
            
            faces[0] = _faces[indexNewFirstValue];
            faces[faces.Count - 1] = _faces[indexNewLastValue];
            
            TurnFaces(turnRight, turnLeft);
        }

        private void MoveUp(List<Face> faces, ref Face faceUp, ref Face faceDown)
        {
            SetFaces(faces, ref faceDown, ref faceUp);

            TurnFaces(faces[IndexRightFaceCub], faces[IndexLeftFaceCub], faceDown, faces[IndexRearFaceCub]);
        }

        private void MoveDown(List<Face> faces, ref Face faceUp, ref Face faceDown)
        {
            SetFaces(faces, ref faceUp, ref faceDown);

            TurnFaces(faces[IndexLeftFaceCub], faces[IndexRightFaceCub], faceUp, faces[IndexRearFaceCub]);
        }

        private void TurnFaces(Face turnRight, Face turnLeft, Face invert = null, Face rearFace = null)
        {
            turnRight?.TurnRight();
            turnLeft?.TurnLeft();
            invert?.Invert();
            rearFace?.Invert();
        }

        private void SetFaces(List<Face> faces, ref Face activFaceCub, ref Face rearFaceCub)
        {
            _currentFace = faces[IndexActivFaceCub];

            faces[IndexActivFaceCub] = activFaceCub;
            activFaceCub = faces[IndexRearFaceCub];
            faces[IndexRearFaceCub] = rearFaceCub;
            rearFaceCub = _currentFace;
        }
    }
}
