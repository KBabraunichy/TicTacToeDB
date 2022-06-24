interface IRepository<T>
{
    public List<T> GetObjectList();
    public T GetObject(int id);
    public void Create(T item);
    public void Update(T item);
    public void Delete(int id);
    public void Save();
    public bool HasUnsavedChanges();

}