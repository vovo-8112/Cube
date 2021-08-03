using ModestTree;
using UnityEngine;

namespace Zenject.Tests
{
    public class SceneContextEventsTester : MonoBehaviour
    {
        [SerializeField]
        SceneContext _sceneContext = null;

        bool _calledPreInstall;
        bool _calledPostInstall;
        bool _calledPreResolve;
        bool _calledPostResolve;

        public void Awake()
        {
            Assert.That(!_sceneContext.HasResolved);
            Assert.That(!_sceneContext.HasInstalled);

            _sceneContext.PreInstall += OnPreInstall;
            _sceneContext.PostInstall += OnPostInstall;
            _sceneContext.PreResolve += OnPreResolve;
            _sceneContext.PostResolve += OnPostResolve;
        }

        public void Start()
        {
            Assert.That(_calledPreInstall);
            Assert.That(_calledPostInstall);
            Assert.That(_calledPreResolve);
            Assert.That(_calledPostResolve);
        }

        void OnPreInstall()
        {
            _calledPreInstall = true;
            
            Debug.Log(_calledPreInstall);
            Assert.IsNotNull(_sceneContext.Container);
        }

        void OnPostInstall()
        {
            _calledPostInstall = true;
            Debug.Log(_calledPreInstall);

        }

        void OnPreResolve()
        {
            _calledPreResolve = true;
            Debug.Log(_calledPreInstall);

        }

        void OnPostResolve()
        {
            _calledPostResolve = true;
            Debug.Log(_calledPreInstall);

        }
    }
}
