public class BaseEvent
{
    public BaseEvent(object listener, object[] args)
    {
        this.m_listener = listener;
        this.m_args = args;
    }
    protected object[] m_args;
    public object[] args { get { return m_args; } }
    protected object m_listener;
    public object listener { get { return m_listener; } }
}
