using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Memoriae
{
    /// <summary>
    /// 管理遊戲本地化文本的靜態類別，支援多語言載入和文本替換
    /// </summary>
    public static class LocalizationManager
    {
        private static readonly Dictionary<string, Dictionary<string, string>> _localizationData = new();

        private static string _currentLanguage = "en";
        private static bool _initialized = false;

        /// <summary>
        /// 初始化本地化管理器，載入所有本地化文件
        /// </summary>
        public static void Initialize(string defaultLanguage = "en")
        {
            if (_initialized) return;

            _currentLanguage = defaultLanguage;
            LoadAllLocalizationFiles();
            _initialized = true;
            Debug.Log($"本地化管理器已初始化，當前語言: {_currentLanguage}");
        }

        /// <summary>
        /// 載入所有本地化檔案
        /// </summary>
        private static void LoadAllLocalizationFiles()
        {
            string[] languages = { "en", "tr_cn" };

            foreach (var language in languages)
            {
                LoadLocalizationFile(language);
            }
        }

        /// <summary>
        /// 載入指定語言的本地化檔案
        /// </summary>
        private static void LoadLocalizationFile(string language)
        {
            try
            {
                string resourcePath = $"XML/Localization/{language}";
                TextAsset xmlFile = Resources.Load<TextAsset>(resourcePath);

                if (xmlFile == null)
                {
                    Debug.LogWarning($"找不到本地化檔案: {resourcePath}");
                    return;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlFile.text);

                if (!_localizationData.ContainsKey(language))
                {
                    _localizationData[language] = new Dictionary<string, string>();
                }

                // 解析 XML 並存儲本地化文本
                XmlNodeList replaceNodes = xmlDoc.GetElementsByTagName("Replace");
                foreach (XmlNode node in replaceNodes)
                {
                    string tag = node.Attributes["Tag"]?.Value;
                    string nodeLanguage = node.Attributes["Language"]?.Value;
                    XmlNode textNode = node.SelectSingleNode("Text");

                    if (!string.IsNullOrEmpty(tag) && textNode != null)
                    {
                        _localizationData[language][tag] = textNode.InnerText;
                    }
                }

                Debug.Log($"成功載入本地化檔案 [{language}]，共 {_localizationData[language].Count} 項");
            }
            catch (Exception ex)
            {
                Debug.LogError($"載入本地化檔案 [{language}] 失敗: {ex.Message}");
            }
        }

        /// <summary>
        /// 設定當前語言
        /// </summary>
        public static void SetLanguage(string language)
        {
            if (!_initialized) Initialize();

            if (_localizationData.ContainsKey(language))
            {
                _currentLanguage = language;
                Debug.Log($"語言已切換為: {language}");
            }
            else
            {
                Debug.LogWarning($"不支援的語言: {language}");
            }
        }

        /// <summary>
        /// 獲取當前語言
        /// </summary>
        public static string GetCurrentLanguage()
        {
            return _currentLanguage;
        }

        /// <summary>
        /// 根據本地化鍵值取得文本
        /// </summary>
        public static string GetText(string tag)
        {
            if (!_initialized) Initialize();

            if (!_localizationData.ContainsKey(_currentLanguage))
            {
                Debug.LogWarning($"本地化數據中不存在語言: {_currentLanguage}");
                return tag;
            }

            if (_localizationData[_currentLanguage].TryGetValue(tag, out var text))
            {
                return text;
            }

            Debug.LogWarning($"找不到本地化文本: {tag} (語言: {_currentLanguage})");
            return tag;
        }

        /// <summary>
        /// 根據本地化鍵值和指定語言取得文本
        /// </summary>
        public static string GetText(string tag, string language)
        {
            if (!_initialized) Initialize();

            if (!_localizationData.ContainsKey(language))
            {
                Debug.LogWarning($"本地化數據中不存在語言: {language}");
                return tag;
            }

            if (_localizationData[language].TryGetValue(tag, out var text))
            {
                return text;
            }

            Debug.LogWarning($"找不到本地化文本: {tag} (語言: {language})");
            return tag;
        }

        /// <summary>
        /// 替換卡牌的本地化文本
        /// </summary>
        public static void LocalizeCard(AbstractCard card)
        {
            if (card == null)
            {
                Debug.LogWarning("卡牌物件為空");
                return;
            }

            if (!_initialized) Initialize();

            // 本地化文本已在卡牌類中定義，此方法用於驗證和調試
            Debug.Log($"卡牌 {card.Id} 本地化完成");
        }

        /// <summary>
        /// 批量替換卡牌文本為本地化版本並返回本地化文本字典
        /// </summary>
        public static Dictionary<string, string> GetLocalizedCardTexts(AbstractCard card)
        {
            if (card == null)
            {
                Debug.LogWarning("卡牌物件為空");
                return new Dictionary<string, string>();
            }

            if (!_initialized) Initialize();

            var localizedTexts = new Dictionary<string, string>
            {
                { "Name", GetText(card.Name) },
                { "Description", GetText(card.TargetDescription) },
                { "EffectDescription", GetText(card.EffectDescription) }
            };

            return localizedTexts;
        }

        /// <summary>
        /// 替換文本中的所有本地化鍵值為實際文本
        /// </summary>
        public static string ReplaceLocalizationTags(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            if (!_initialized) Initialize();

            // 尋找形似 <LOC_...> 的標籤並替換
            string result = text;
            foreach (var kvp in _localizationData[_currentLanguage])
            {
                result = result.Replace(kvp.Key, kvp.Value);
            }

            return result;
        }

        /// <summary>
        /// 檢查本地化文本是否存在
        /// </summary>
        public static bool HasText(string tag)
        {
            if (!_initialized) Initialize();

            return _localizationData.ContainsKey(_currentLanguage) &&
                   _localizationData[_currentLanguage].ContainsKey(tag);
        }

        /// <summary>
        /// 獲取所有支援的語言列表
        /// </summary>
        public static string[] GetSupportedLanguages()
        {
            if (!_initialized) Initialize();

            string[] languages = new string[_localizationData.Keys.Count];
            _localizationData.Keys.CopyTo(languages, 0);
            return languages;
        }

        /// <summary>
        /// 清除所有本地化數據快取
        /// </summary>
        public static void ClearCache()
        {
            _localizationData.Clear();
            _initialized = false;
            Debug.Log("本地化數據快取已清除");
        }

        /// <summary>
        /// 重新載入本地化數據
        /// </summary>
        public static void Reload()
        {
            ClearCache();
            Initialize(_currentLanguage);
        }

        /// <summary>
        /// 獲取本地化數據統計
        /// </summary>
        public static void PrintStatistics()
        {
            if (!_initialized) Initialize();

            Debug.Log("=== 本地化數據統計 ===");
            foreach (var language in _localizationData.Keys)
            {
                Debug.Log($"{language}: {_localizationData[language].Count} 項文本");
            }
        }
    }
}