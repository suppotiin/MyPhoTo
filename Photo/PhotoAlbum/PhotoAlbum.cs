using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PhotoAlbum
{
    class PhotoAlbum : Collection<Photo>, IDisposable
    {
        private bool hasChanged = false;
        public bool HasChanged
        {
            get
            {
                if (hasChanged) return true;
                foreach (Photo p in this)
                    if (p.HasChanged) return true;
                return false;
            }
            set
            {
                hasChanged = value;
                if (value == false)
                    foreach (Photo p in this)
                        p.HasChanged = false;
            }
        }

        public void Dispose()
        {
            foreach (Photo p in this)
                p.Dispose();
        }

        public Photo Add(string fileName)
        {
            Photo p = new Photo(fileName);
            base.Add(p);
            return p;
        }
        protected override void ClearItems()
        {
            if (Count > 0)
            {
                Dispose();
                base.ClearItems();
                HasChanged = true;
            }
        }
        protected override void InsertItem(int index, Photo item)
        {
            base.InsertItem(index, item);
            HasChanged = true;
        }
        protected override void RemoveItem(int index)
        {
            Items[index].Dispose();
            base.RemoveItem(index);
            hasChanged = true;
        }
        protected override void SetItem(int index, Photo item)
        {
            base.SetItem(index, item);
            HasChanged = true;
        }
    }

}
