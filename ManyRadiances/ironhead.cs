

namespace ManyRadiances
{
    public class ironhead:MonoBehaviour
    {
        private bool yes = false;
        private void Update()
        {
            if (!yes) { base.gameObject.AddComponent<DamageHero>().damageDealt = 2; yes = true; }
        }
        private void Ondestroy()
        {
            yes = false;
        }
    }
}
