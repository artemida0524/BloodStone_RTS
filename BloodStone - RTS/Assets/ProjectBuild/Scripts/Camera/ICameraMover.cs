namespace GameCamera
{
    public interface ICameraMover
    {
        bool IsMoving { get; }
        void Move();
    }
}