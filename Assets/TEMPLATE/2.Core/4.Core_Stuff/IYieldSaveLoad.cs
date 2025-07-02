using System.Collections;

namespace Monster
{
    public interface IYieldSaveLoad
    {
        public abstract void Save();

        public abstract IEnumerator LoadData();
    }
}

