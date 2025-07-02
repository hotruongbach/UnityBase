using System.Collections;

namespace Template
{
    public interface IYieldSaveLoad
    {
        public abstract void Save();

        public abstract IEnumerator LoadData();
    }
}

