using System.Collections;
using _2048Figure.Architecture.ServiceLocator;
using InstantGamesBridge;
using TMPro;
using UnityEngine;
using DeviceType = InstantGamesBridge.Modules.Device.DeviceType;

namespace DrakraVParke.Player
{
    public enum TutorialEnum
    {
        SingleAttack = 1,
        ComboAttack = 2,
        UpperCut = 3,
        JumpDownKick = 4,
        Done = 5
    }

    public class Tutorial : MonoBehaviour, IService
    {
        public TutorialEnum _enum;
        [SerializeField] private GameObject _tutor;
        [SerializeField] private GameObject _tutorDesktop;
        [SerializeField] private GameObject _tutorMobile;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject skipBtn;
        [SerializeField] private StatsView _statsView;
        public bool isDone = false;
        private bool m_next = true;
        
        private void Awake()
        {
            skipBtn.SetActive(false);
            _statsView.Init();
            if (PlayerPrefs.GetInt("Totor") == 1)
            {
                isDone = true;
                return;
            }
            if (Bridge.device.type == DeviceType.Desktop)
                _tutor = _tutorDesktop;
            else
                _tutor = _tutorMobile;
            _enum = TutorialEnum.SingleAttack;
            
            text.gameObject.SetActive(true);
            _tutor.SetActive(true);
            skipBtn.SetActive(true);
        }

        private void Update()
        {
            if (isDone)
                return;
            
            switch (_enum)
            {
                case TutorialEnum.SingleAttack:
                    if(m_next)
                        SingleAttack();
                    break;
                case TutorialEnum.ComboAttack:
                    if(m_next)
                        ComboAttack();
                    break;
                case TutorialEnum.UpperCut:
                    if(m_next)
                        UpperCut();
                    break;
                case TutorialEnum.JumpDownKick:
                    if(m_next)
                        JumpDownAttack();
                    break;
                case TutorialEnum.Done:
                    Done();
                break;
            }
        }
        
        

        private void SingleAttack()
        {
            text.text = "Для атаки нажми левую стрелочку или правую!";
            
            text.text = "Для атаки нажми справа или слева!";
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                StartCoroutine(UpdateTutor());
            }
        }

        private int m_comboSubnet = 0;
        private void ComboAttack()
        {
            text.text = "Для комбо атаки нажми быстро несколько раз!";
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                m_comboSubnet++;
            if(m_comboSubnet >= 4)
                StartCoroutine(UpdateTutor());
        }

        private bool _sitDown;
        private void UpperCut()
        {
            if (Bridge.device.type == DeviceType.Desktop)
            {
                text.text = "Для апперкота присядь, с помощью стрелочки вниз и нажми стелочку вверх!";
            
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _sitDown = true;
                }
                if (_sitDown)
                {
                    if(Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        _sitDown = false;
                        StartCoroutine(UpdateTutor());
                    }
                }
            }
            else
            {
                text.text = "Для апперкота присядь, с помощью свайпа вниз и сделай свайп вверх!";
                CheckSwap();
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (Y > 3 && _sitDown)
                    {
                        _sitDown = false;
                        StartCoroutine(UpdateTutor());
                    }
                    else if (Y < -3 && Y < 0)
                    {
                        _sitDown = true;
                    }
                    Y = 0;
                }
            }
        }

        private bool _jump;
        private void JumpDownAttack()
        {
            if (Bridge.device.type == DeviceType.Desktop)
            {
                text.text = "Для атаки вниз в прыжке, подпрыгни с помощью стрелочки вверх и нажми стрелочку вниз!";
            
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _jump = true;
                    StartCoroutine(ResetJump());
                }
                if (_jump)
                {
                    if(Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        StartCoroutine(UpdateTutor());
                    }
                }
            }
            else
            {
                text.text = "Для атаки вниз в прыжке, подпрыгни с помощью свайпа вверх и сделай свайп вниз!";
                CheckSwap();
                if (_jump)
                {
                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        if (Y < -3)
                        {
                            StartCoroutine(UpdateTutor());
                        }
                    }
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended  && !_jump)
                {
                    if (Y > 3 && !_sitDown && !_jump)
                    {
                        _jump = true;
                        StartCoroutine(ResetJump());
                    }
                }
            }
        }

        private IEnumerator ResetJump()
        {
            yield return new WaitForSeconds(0.5f);
            _jump = false;
        }

        private void Done()
        {
            StartCoroutine(EndTutor());
        }

        private IEnumerator EndTutor()
        {
            _tutor.SetActive(false);
            skipBtn.SetActive(false);
            m_next = false;
            text.text = "Удачи в игре!";
            yield return new WaitForSeconds(2f);
            _tutor.SetActive(false);
            text.gameObject.SetActive(false);
            isDone = true;
            PlayerPrefs.SetInt("Totor", 1);
        }

        private int currentAnim = 1;
        private IEnumerator UpdateTutor()
        {
            _tutor.SetActive(false);
            m_next = false;
            text.text = "МОЛОДЕЦ!";
            _enum++;
            yield return new WaitForSeconds(2f);
            m_next = true;
            _tutor.SetActive(true);
            currentAnim++;
            _tutor.GetComponent<Animator>().Play(currentAnim.ToString());
            
        }

        private float Y;
        private void CheckSwap()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Y = -Input.GetTouch(0).deltaPosition.y;
            }
        }

        public void SkipTutor()
        {
            Done();
        }
    }
}