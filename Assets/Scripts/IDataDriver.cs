using UnityEngine;

public interface IDataDriver
{
    bool SaveData<T>(string path, T Data, bool Encrypted);

    T LoadData<T>(string path, bool Encrypted);
}
