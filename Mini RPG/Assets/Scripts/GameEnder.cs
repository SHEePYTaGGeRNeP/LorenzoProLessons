using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class GameEnder : MonoBehaviour
    {

        public void OnPlayerHealthChanged(HealthChangeEventArgs e)
        {
            if (e.CurrentHitPoints != 0)
                return;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
