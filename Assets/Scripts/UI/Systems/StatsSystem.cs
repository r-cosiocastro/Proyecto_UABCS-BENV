using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using System;

public class StatsSystem {
    public event EventHandler OnChangedData;
    public event EventHandler OnIntro;
    public event EventHandler OnCorrectAnswer;
    public event EventHandler OnIncorrectAnswer;
    private ResultEntity results;
    private PlayerSystem playerSystem;

    public StatsSystem(PlayerSystem playerSystem) {
        this.playerSystem = playerSystem;
    }

    public string GetCurrentPlayerName() {
        return playerSystem.GetName();
    }

    public int GetCorrectAnswersUser() {
        return results._correct;
    }

    public int GetIncorrectAnswersUser() {
        return results._incorrect;
    }

    public int GetCorrectAnswersGroup() {
        return results._groupCorrect;
    }

    public int GetIncorrectAnswersGroup() {
        return results._groupIncorrect;
    }

    public void AddCorrectAnswer() {
        results.AddCorrectAnswer();
        OnCorrectAnswer?.Invoke(this, EventArgs.Empty);
        OnChangedData?.Invoke(this, EventArgs.Empty);
    }

    public void AddIncorrectAnswer() {
        results.AddIncorrectAnswer();
        OnIncorrectAnswer?.Invoke(this, EventArgs.Empty);
        OnChangedData?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeStats(ResultEntity resultEntity) {
        results = resultEntity;

        OnChangedData?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeStats(StudentEntity studentEntity, ObjectEntity objectEntity) {
        results = results.GetResults(studentEntity, objectEntity);

        OnChangedData?.Invoke(this, EventArgs.Empty);
    }

    public void ShowIntro() {
        OnIntro?.Invoke(this, EventArgs.Empty);
    }
}
