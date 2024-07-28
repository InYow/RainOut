using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_01 : Skill
{
    int i;

    private void Awake()
    {
        i = 2; // 初始化i的值
    }

    public override void Effect()
    {
        StartCoroutine(IEAttackTwice()); // 使用方法名称而不是字符串
    }

    private IEnumerator IEAttackTwice()
    {
        while (i > 0)
        {
            Attack(origin, target, 25); // 执行攻击
            AudioPlay(); // 播放音频

            i--; // 递减i

            yield return new WaitForSeconds(0.2f); // 等待0.2秒
        }

        // 重置i的值，确保下一次调用时能正确执行
        i = 2;
    }

}
