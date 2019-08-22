using UnityEngine;

namespace Game
{
    public class FigurePoolManager : MonoBehaviour, IManager
    {
        private GameObject[] figurePool;
        private GameObject secretFigurePool;
        private GameObject[] allFigures;
        private int secretPoolCount = 9;

        public void Manage()
        {
            if (allFigures == null)
            {
                allFigures = Resources.LoadAll<GameObject>("Prefabs/Shapes");
            }

            if (figurePool == null)
            {
                figurePool = GameObject.FindGameObjectsWithTag("Pool");
            }

            if (secretFigurePool == null)
            {
                secretFigurePool = GameObject.FindWithTag("SecretPool");
            }

            if (secretFigurePool.transform.childCount <= 0)
            {
                FillSecretPool();
            }

            FillPool();
        }

        private void FillPool()
        {
            foreach (var pool in figurePool)
            {
                Transform figure =
                    secretFigurePool.transform.GetChild(Random.Range(0, secretFigurePool.transform.childCount));
                figure.SetParent(pool.transform);
                figure.position = figure.parent.position;
            }
        }

        private void FillSecretPool()
        {
            for (int i = 0; i < secretPoolCount; i++)
            {
                Instantiate(
                    allFigures[Random.Range(0, allFigures.Length)],
                    secretFigurePool.transform.position,
                    Quaternion.Euler(0,0,Random.Range(0,4)*90),
                    secretFigurePool.transform
                    );
            }
        }
    }
}