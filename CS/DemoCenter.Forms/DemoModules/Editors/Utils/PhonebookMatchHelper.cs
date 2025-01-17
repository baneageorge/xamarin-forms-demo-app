/*
               Copyright (c) 2015-2020 Developer Express Inc.
{*******************************************************************}
{                                                                   }
{       Developer Express Mobile UI for Xamarin.Forms               }
{                                                                   }
{                                                                   }
{       Copyright (c) 2015-2020 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING         }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace DemoCenter.Forms.Views {
    public class PhonebookMatchHelper {
        static string[] phoneLetters = new[] {
                "", "", "[abcABC]", "[defDEF]", "[ghiGHI]", "[jklJKL]", "[mnoMNO]", "[pqrsPQRS]", "[tuvTUV]", "[wxyzWXYZ]"
        };

        public static PhoneBookEntryMatch MatchPhoneNumber(string phoneNumber, string input) {
            if (String.IsNullOrEmpty(input))
                return null;
            List<char> digits = new List<char>();
            List<int> indexes = new List<int>();
            int nextIndex = 0;
            foreach (char c in phoneNumber) {
                if (Char.IsDigit(c)) {
                    digits.Add(c);
                    indexes.Add(nextIndex);
                }
                nextIndex++;
            }
            if (digits.Count == 0)
                return null;
            string phoneNumberDigits = new String(digits.ToArray());
            string inputDigits = new String(input.Where(c => Char.IsDigit(c)).ToArray());
            if (String.IsNullOrEmpty(inputDigits))
                return null;
            int inputIndex = phoneNumberDigits.IndexOf(inputDigits);
            if (inputIndex == -1)
                return null;
            int startIndex = indexes[inputIndex];
            int endIndex = indexes[inputIndex + inputDigits.Length - 1];
            return new PhoneBookEntryMatch(phoneNumber, startIndex, endIndex - startIndex + 1);
        }

        public static PhoneBookEntryMatch MatchPhoneLetters(string fullName, string input) {
            if (String.IsNullOrEmpty(input))
                return null;
            StringBuilder builder = new StringBuilder();
            foreach (char c in input) {
                if (!Char.IsDigit(c))
                    continue;
                builder.Append(phoneLetters[c - '0']);
            }
            string inputString = builder.ToString();
            if (inputString.Length == 0) {
                int startIndex = fullName.IndexOf(input);
                if (startIndex == -1)
                    return null;
                return new PhoneBookEntryMatch(fullName, startIndex, input.Length);
            }
            Regex phoneLettersRegex = new Regex(builder.ToString());
            Match match = phoneLettersRegex.Match(fullName);
            if (!match.Success)
                return null;
            return new PhoneBookEntryMatch(fullName, match.Index, match.Length);
        }
    }

    public class PhoneBookEntryMatch {
        internal PhoneBookEntryMatch(string entryField, int index, int length) {
            EntryField = entryField;
            Index = index;
            Length = length;
        }

        public string EntryField { get; }
        public int Index { get; }
        public int Length { get; }

        public FormattedString AsFormattedString(Color matchHighlightColor) {
            FormattedString formattedString = new FormattedString();
            if (Index != 0)
                formattedString.Spans.Add(new Span() { Text = EntryField.Substring(0, Index) });
            formattedString.Spans.Add(new Span() { Text = EntryField.Substring(Index, Length), ForegroundColor = matchHighlightColor });
            int endIndex = Index + Length;
            if (endIndex != EntryField.Length)
                formattedString.Spans.Add(new Span() { Text = EntryField.Substring(endIndex) });
            return formattedString;
        }
    }
}
