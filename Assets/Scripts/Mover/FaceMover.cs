using Main;
using System.Collections.Generic;

namespace Mover {
    public class FaceMover
    {
        private const int IndexActivFaceCub = 0;
        private const int IndexRightFaceCub = 1;
        private const int IndexRearFaceCub = 2;
        private const int IndexLeftFaceCub = 3;

        private Face _currentFace;

        public void MoveLeft(List<Face> faces, ref Face faceUp, ref Face faceDown)
        {
            _currentFace = faces[IndexActivFaceCub];

            for (int i = 0; i < faces.Count - 1; i++)
            {
                faces[i] = faces[i + 1];
            }

            faces[faces.Count - 1] = _currentFace;

            TurnFaces(faceUp, faceDown);
        }

        public void MoveRight(List<Face> faces, ref Face faceUp, ref Face faceDown)
        {
            _currentFace = faces[faces.Count - 1];

            for (int i = faces.Count - 1; i > 0; i--)
            {
                faces[i] = faces[i - 1];
            }

            faces[IndexActivFaceCub] = _currentFace;

            TurnFaces(faceDown, faceUp);
        }

        public void MoveUp(List<Face> faces, ref Face faceUp, ref Face faceDown)
        {
            _currentFace = faces[IndexActivFaceCub];

            faces[IndexActivFaceCub] = faceDown;
            faceDown = faces[IndexRearFaceCub];
            faces[IndexRearFaceCub] = faceUp;
            faceUp = _currentFace;

            TurnFaces(faces[IndexRightFaceCub], faces[IndexLeftFaceCub], faceDown, faces[IndexRearFaceCub]);
        }

        public void MoveDown(List<Face> faces, ref Face faceUp, ref Face faceDown)
        {
            _currentFace = faces[IndexActivFaceCub];

            faces[IndexActivFaceCub] = faceUp;
            faceUp = faces[IndexRearFaceCub];
            faces[IndexRearFaceCub] = faceDown;
            faceDown = _currentFace;

            TurnFaces(faces[IndexLeftFaceCub], faces[IndexRightFaceCub], faceUp, faces[IndexRearFaceCub]);
        }

        private void TurnFaces(Face turnRight, Face turnLeft, Face invert = null, Face rearFace = null)
        {
            turnRight?.TurnRight();
            turnLeft?.TurnLeft();
            invert?.Invert();
            rearFace?.Invert();
        }
    }
}
