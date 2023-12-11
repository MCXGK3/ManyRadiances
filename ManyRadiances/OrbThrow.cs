
namespace Dumb
{
    // Token: 0x02000007 RID: 7
    internal class OrbThrow : MonoBehaviour
    {
        // Token: 0x06000033 RID: 51 RVA: 0x00004DF0 File Offset: 0x00002FF0
           

        private void OnTriggerEnter2D(Collider2D col)
        {
            bool flag = col.name == "Floor Saver" || col.name == "Roof Collider" || col.name == "Terrain Saver";
            bool flag2 = flag;
            if (flag2)
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
        }

        // Token: 0x06000034 RID: 52 RVA: 0x00004E4C File Offset: 0x0000304C
        private void Start()
        {
            base.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            base.gameObject.GetComponent<Rigidbody2D>().rotation = 57.29578f * Mathf.Atan(base.gameObject.GetComponent<Rigidbody2D>().velocity.y / base.gameObject.GetComponent<Rigidbody2D>().velocity.x);
            this.time = 1.5f;
        }

        // Token: 0x06000035 RID: 53 RVA: 0x00004ECC File Offset: 0x000030CC
        private void FixedUpdate()
        {
            bool flag = this.time <= 1.5f;
            bool flag2 = flag;
            if (flag2)
            {
                this.LookTowards(this.angle * Time.deltaTime);
                this.speed += 0.1f;
                base.gameObject.transform.position += base.gameObject.transform.right * this.speed * Time.deltaTime;
            }
            this.time -= Time.deltaTime;
            bool flag3 = this.time <= 0f;
            bool flag4 = flag3;
            if (flag4)
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
        }

        // Token: 0x06000036 RID: 54 RVA: 0x00004F90 File Offset: 0x00003190
        public void LookTowards(float rotationSpeed)
        {
            Vector3 vector = this.target.transform.position - base.gameObject.transform.position;
            vector.Normalize();
            float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
            base.gameObject.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.AngleAxis(num, Vector3.forward), rotationSpeed);
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00005011 File Offset: 0x00003211
        private static void Log(object obj)
        {
            Modding.Logger.Log("[Orb Throw] " + ((obj != null) ? obj.ToString() : null));
        }

        // Token: 0x04000030 RID: 48
        private float time = 3f;

        // Token: 0x04000031 RID: 49
        private Vector2 oldDirect;

        // Token: 0x04000032 RID: 50
        public GameObject target = HeroController.instance.gameObject;

        // Token: 0x04000033 RID: 51
        public float angle = 170f;

        // Token: 0x04000034 RID: 52
        public float speed = 13f;

        // Token: 0x04000035 RID: 53
        private bool first;
    }
}
