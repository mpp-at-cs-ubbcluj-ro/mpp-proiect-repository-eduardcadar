namespace Model
{
    interface IIdentifiable<Tid>
    {
        Tid getId();
        void setId(Tid id);
    }
}
