using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtils
{
    public static string FormatTextDialog(string formatThisString, string objectName, string studentName){
		return string.Format(formatThisString,
				DialogFormatStudentName(studentName), 
				objectName, 
				DialogFormatObjectName(objectName),
				DialogFormatObjectSpell(objectName));
	}
	public static string FormatTextTTS(string formatThisString, string objectName, string studentName){
		return string.Format(formatThisString,
				studentName, 
				objectName, 
				TTSEmphObjectName(objectName),
				TTSSpellObjectName(objectName));
	}

    public static string TTSSpellObjectName(string objectName){
        return "<spell>"+objectName+"</spell>";
    }
    
    public static string TTSEmphObjectName(string objectName){
        return "<emph>"+objectName+"</emph>";
    }

    public static string DialogFormatObjectSpell(string objectName){
        return "<sp:8><b><anim:wave>"+objectName.ToUpper()+"</anim></b>";
    }

    public static string DialogFormatStudentName(string studentName){
        return "<color=#2980B9>"+studentName+"</color>";
    }

    public static string DialogFormatObjectName(string objectName){
        return "<color=#D35400><b>"+objectName+"</b></color>";
    }
}
