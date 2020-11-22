using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace BFS
{
    public class BFSSolver
    {
        private List<Field> _closed = new List<Field>();
        private List<Field> _open = new List<Field>();
        public BFSSolver(Field startingField)
        {
            _open.Add(startingField);
        }
        public List<Field> Search()
        {
            Field current = _open.First();
            while (!current.IsSolved())
            {
                if (current.MoveRightAvailable())
                    HandleChild(
                        current.MoveRight());
                if (current.MoveLeftAvailable())
                    HandleChild(
                        current.MoveLeft());
                if (current.MoveUpAvailable())
                    HandleChild(
                        current.MoveUp());
                if (current.MoveDownAvailable())
                    HandleChild(
                        current.MoveDown());

                _open = _open.OrderBy(f => f.Distance).ToList();
                _open.Remove(current);
                _closed.Add(current);
                current = _open[0];
                
            }
            _closed.Add(current);
            return RestoreContinuity();
        }
        private void HandleChild(Field child)
        {
            Field previousOpen = _open.FirstOrDefault(f => f.Equals(child));
            Field previousClosed = _open.FirstOrDefault(f => f.Equals(child));
            if (previousOpen == null && previousClosed == null)
            {
                _open.Add(child);
                return;
            }
            if (previousOpen != null)
            {
                if (child.Distance < previousOpen.Distance)
                {
                    _open.Remove(previousOpen);
                    _open.Add(child);
                    return;
                }
            }
            if (previousClosed != null)
            {
                if (child.Distance < previousClosed.Distance)
                {
                    _closed.Remove(previousClosed);
                    _open.Add(child);
                    return;
                }
            }

        }
        private List<Field> RestoreContinuity()
        {
            List<Field> fieldContinuity = new List<Field>();
            Field currentField = _closed.Last();
            fieldContinuity.Add(currentField);
            while(currentField.ParentId != default)
            {
                currentField = _closed.FirstOrDefault(
                    f => f.Id == currentField.ParentId);
                fieldContinuity.Add(currentField);
            }
            fieldContinuity.Reverse();
            return fieldContinuity;
        }
    }
}
