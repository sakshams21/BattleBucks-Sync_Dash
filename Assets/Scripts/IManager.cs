using System.Collections;

interface IManager<T>
{
    public IEnumerator GeneratePool(int maxPoolCount);
    public T GetItem();

    public void BackToPool(T item);

}