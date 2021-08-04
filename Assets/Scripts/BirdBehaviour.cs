//старайся убирать неиспользуемые нейспейсы, в данном случае System.Collections и System.Collections.Generic. 
//меньше ненужных зависимостей -- легче билд. райдер подсвечивает неиспользуемое серым
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    
    public float horizontalSpeed; // пустую строку сверху лучше убирать, читается лучше 
    public float verticalForse; //старайся избегать опечаток
    public float yVelosity; //тут тоже
    public Vector2 jumpDirection;
    //объявляй все внутренние поля private, если их не нужно явно изменять из других мест
    //чтобы их можно было бы твикать в эдиторе, используй атрибут SerializeField
    //подробнее что это: https://docs.unity3d.com/ScriptReference/SerializeField.html
    
    //+ публичные поля обычно пишут UpperCase, приватные -- lowerCase или _lowerCase
    
    //есть разные мнения насчет необходимости private, но лично мне кажется что их лучше ставить, так код выглядит чище
    Rigidbody2D rb;
    // Start is called before the first frame update <--- это тоже сносить можно
    void Start()
    {
        //this здесь избыточен
        rb=this.GetComponent<Rigidbody2D>();
        //есть мнения, что лучше избегать GetComponent, если ты можешь затянуть поле ручками в эдиторе, это ускоряет загрузку прилажки
        //но я таким тоже грешу, и топтал эти мнения :) в любом случае такое лучше выносить в Awake(), т.к. это момент инициализации
        //подробнее про порядок вызова юнитевых методов: https://docs.unity3d.com/Manual/ExecutionOrder.html
    }

    private void FixedUpdate()
    {
        //а зачем тут yVelosity? ты же никак не используешь потом этот параметр. меньше филдов -- больше памяти! 
        yVelosity = rb.velocity.y;
        //если работаешь с rigidbody, но менять вручную ускорение так себе идея
        //и я пока не знаю что предложить в данном случае, мб зафиксировать в rgb Ox и двигать прямолинейно в апдейте
        rb.velocity = new Vector2(horizontalSpeed, yVelosity);
        //считывать инпут пользователя в FixedUpdate в данном случае -- плохая идея
        //попробуй поиграть, заметишь, что иногда нажатие на пробел никак не трекается, а все из-за того, что
        //FixedUpdate вызывается 100 раз в секунду, а вот Update (на котором и трекается инпут) сильно реже
        //подробнее: https://forum.unity.com/threads/check-for-user-input-in-fixedupdate.214706/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(horizontalSpeed, 0);
            //зачем нужен jumpDirection? он всегда (0;1)
            rb.AddForce(jumpDirection * verticalForse, ForceMode2D.Impulse);
        }
        
    } //между методами одна пустая строка должна быть
    void Update()
    {

        
    }
    //если не используется какой-то из системных методов, то его нужно удалять. в данном случае Update
    //юнити дергает все эти методы, даже если они пустые 
    //подробнее -- https://gamedev.stackexchange.com/a/164904/75627
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //вместо такого сравнения нужно использовать метод CompareTag, а сам тэг вынести в константу 
        //rider тебе с этим поможет, кстати. он подсвечивает такие моменты волнистой желтой линией и сам фиксит по нажаюти на Alt+Enter (Option+Enter на маке)
        if (collision.transform.tag == "Wall")
        {
            //horizontalSpeed лучше оставить константой, и добавить переменную направления, и уже ее менять
            horizontalSpeed *= -1;
            rb.velocity = new Vector2(horizontalSpeed, 0);
            rb.AddForce(jumpDirection*4, ForceMode2D.Impulse); //что такое 4? magic numbers в коде не должно быть, если нужна константа -- выноси константой
            //у тебя в предыдущем методе как раз verticalForce для этого используется 
            //если хочешь чтобы он стенки отскакивало сильнее, то тогда используй verticalForceAfterWallCollision или какой-нибудь мультипликатор
        }
    }
}
