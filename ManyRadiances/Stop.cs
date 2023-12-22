namespace ManyRadiances
{
    internal class Stop:MonoBehaviour
    {
        private void OnCollisionEnter(Collision col)
        {
            Modding.Logger.Log("BEGIN");
            var rg2b=gameObject.GetComponent<Rigidbody2D>();
            if (rg2b != null)
            {
                rg2b.velocity = Vector3.zero;
            }
            else
            {
                Modding.Logger.Log("FAIL");
            }
            gameObject.LocateMyFSM("Control").SetState("Fly");
            gameObject.RemoveComponent<Stop>();

        }
    }
}