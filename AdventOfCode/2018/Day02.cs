using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2018
{
    internal class Day02
    {
        public void Solve()
        {
            var inputLines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // PART 01
            int doubleChars = 0, tripleChars = 0;
            foreach (string line in inputLines)
            {
                var characterCount = new Dictionary<char, int>();
                for (int i = 0; i < line.Length; i++)
                {
                    char curChar = line[i];
                    if (characterCount.ContainsKey(curChar)) { characterCount[curChar]++; }
                    else { characterCount.Add(curChar, 1); }
                }

                if (characterCount.Count(kvp => kvp.Value == 2) > 0) { doubleChars++; }
                if (characterCount.Count(kvp => kvp.Value == 3) > 0) { tripleChars++; }
            }

            Console.WriteLine($"Day02 part1 answer:{doubleChars * tripleChars}");

            // PART 02
            var countedChars = new Dictionary<string, Dictionary<char, int>>();
            foreach (string line in inputLines)
            {
                var characterCount = new Dictionary<char, int>();
                for (int i = 0; i < line.Length; i++)
                {
                    char curChar = line[i];
                    if (characterCount.ContainsKey(curChar)) { characterCount[curChar]++; }
                    else { characterCount.Add(curChar, 1); }
                }
                countedChars.Add(line, characterCount);
            }

            bool boxFound = false;
            for (int i = 0; i < countedChars.Count; i++)
            {
                if (boxFound) { break; }
                for (int j = i + 1; j < countedChars.Count; j++)
                {
                    // compare dictionaries
                    int differencesInCharacters = 0;
                    var dictionaryToCompare = countedChars[inputLines[j]];
                    foreach (var kvp in countedChars[inputLines[i]])
                    {
                        if (!dictionaryToCompare.ContainsKey(kvp.Key) || dictionaryToCompare[kvp.Key] != kvp.Value)
                        {
                            differencesInCharacters++;
                        }
                        if (differencesInCharacters > 1) { break; }
                    }
                    // potential candidates
                    if (differencesInCharacters == 1)
                    {
                        int positionalDifference = 0;
                        string candidate1 = inputLines[i];
                        string candidate2 = inputLines[j];
                        var sb = new StringBuilder();
                        for (int k = 0; k < candidate1.Length; k++) {
                            if (candidate1[k] != candidate2[k]) { positionalDifference++; }
                            else { sb.Append(candidate1[k]); }
                        }
                        if (positionalDifference == 1) {
                            Console.WriteLine($"Day02 part2 answer: {sb.ToString()}");
                            boxFound = true;
                        }
                        break;

                    }
                }
            }
        }

        public static string input2 = "bababc";

        public static string input = @"dghfbsyiznoumojleevappwqtr
dghfbsyiznoumkjljevacpmqer
dghfbbyizioumkjlxevacpwdtr
dghfbsesznoumkjlxevacpwqkr
dghfbsynznoumkjlxziacpwqtr
cghfbsyiznjumkjlxevacprqtr
dghfjsyizwoumkjlxevactwqtr
dghfdsyfinoumkjlxevacpwqtr
hghfbsyiznoumkjlxivacpwqtj
dgcfbsyiznoumkjlxevacbuqtr
dghfbsyiznoymnjlxevacpwvtr
dfhfbsyiznoumkulxevacptqtr
dghfasyiznovmkjlxevacpwqnr
dghfbsyihnouikjlxevackwqtr
dghfbayiznolmkjlyevacpwqtr
jghfbsyiznoumnjldevacpwqtr
dhhfbsyuznoumkjlxevakpwqtr
nehfrsyiznoumkjlxevacpwqtr
dghfbsyiznxdmkolxevacpwqtr
dgpfbsyizwlumkjlxevacpwqtr
yghfbsyiznoumkjlsevacpwqtm
dghfssyiznoumkjlxevvcpwqjr
dahfbsyiznoumkjlfevacpwqto
duhfcsyiznouvkjlxevacpwqtr
dghfbvyiznoumkjlrevacpwvtr
dghfgsyiznoumknlxgvacpwqtr
jghfbeyiznkumkjlxevacpwqtr
daofbsyiznoumkjlxevampwqtr
dghfbsyiznojmkjlxeracpcqtr
dghnbsyiznouokjlxevaclwqtr
dgifbsyiznoumkjlxevnspwqtr
dgkfpsziznoumkjlxevacpwqtr
dghfxsyijnoumkjlxevaccwqtr
dghfbsyiznolmkjlwevzcpwqtr
dkhfbsaiznoumkjlxevacpwqtg
dghfbsygknoumkjlaevacpwqtr
dghfbsyizizumkjlxevacpxqtr
ighfbbyijnoumxjlxevacpwqtr
dghfbsyizrouekjlxevacpwktr
dghobsyiznoujkjlxevacnwqtr
dghpbsyizyoumkjlxeaacpwqtr
dghffsyiznoymkjlxevacewqtr
dghkbssiznoumzjlxevacpwqtr
dghfbsyawnoumkjlxevacpwjtr
drhfbsyiznormkjlfevacpwqtr
dghfbsoiznouwkjlxevacpwqtn
dghfmsyiznoumkjlxlvecpwqtr
dxhfbsyiznoumkjlxeeacvwqtr
dghnbsyiznoumkjsxevacpwqur
dghfbsyiznrujkjlxevacpwqtc
dghfbstoznoumhjlxevacpwqtr
dghfboyiznzumkjlvevacpwqtr
dghfbsyiznjumkjlxevgcpwmtr
dghfbsnizaoumkjlxevacpwetr
dghfbsyirnoumkjoxevacplqtr
dghfbsyiznoumkjlxavvckwqtr
dghfbjdiznoumkjlxevacpwptr
dghfbsywznoumkjlxeiacpwqcr
djhfbsyizuoumkjlxelacpwqtr
dghffsniznoumkjlxpvacpwqtr
dghebsyizuoumkjlxevecpwqtr
rghfbsyiznourkjcxevacpwqtr
dghfbsyignoumkwlxenacpwqtr
dghfbsyiznrufkjlxevacpwqth
dgifbsyiznoumkjlxevacpjqnr
dghfbsyiznoumkjbxevaxpwqtw
dsufbsyizncumkjlxevacpwqtr
dihfbsyiznoumujlxecacpwqtr
dghfbiyiznoumkjlxevajpwqtn
dghqbsyixnoumkjlrevacpwqtr
dghfbsyiznouukjlxeuacpwqtx
dghfbsyizyoumksfxevacpwqtr
dghfbsiiznopfkjlxevacpwqtr
eghfbsyidnoumkjlxexacpwqtr
dghfbgyiznouwkjlwevacpwqtr
dghfbsyyznoumkjlxevacwwqtf
bghfbtypznoumkjlxevacpwqtr
dghfbsyiznoumtjlxebacpwetr
dghfbsgiznonmkplxevacpwqtr
dghfbsyiznoumxjlxevanpwqpr
dghfbsyiznwumujuxevacpwqtr
dghxbsyiznoumkjzxevaypwqtr
dghfbsyhznoumkjlxlvacpiqtr
dghfbsyiznoumkjlxevzcnwqrr
dvhfbsyiznoumkjluevacpzqtr
dghcbsyiznoumkjlxmvacpwetr
dghfbsyiznohmkjvxbvacpwqtr
dghfzsyiznouokjlxevacpwqpr
dghfbsyiznoumkjlxevachtqth
dghfbsyiznoumkjlxjvacpfutr
dghfbsyiznoumkjlxevsppwqtt
dghfusyiznouakhlxevacpwqtr
dghfbsyizcoumkjlxrvaipwqtr
dghebsyipnoumfjlxevacpwqtr
dgdfbsyiznoumkjlwevacpkqtr
dghfbsyiznoumkjlcffacpwqtr
dghfbsypznfumkjlxevacpwqar
dghfbsyiznojmkjlxevgcpkqtr
dghfbsyiznoumkjlaevlcpwstr
dgafrsyiunoumkjlxevacpwqtr
dghfbsyiznouqljlxevacrwqtr
dyhkbsyiznokmkjlxevacpwqtr
pghfbsciznoumkjlxevacpwvtr
dghfbxyiznonmkjllevacpwqtr
ighfbsyizxoumkjlxevacpzqtr
dgffbsyoznoumkjlxevacpwqto
hghfbsyiznoumkjlpevachwqtr
dlhfosyiznoumkjldevacpwqtr
dghfbsvizkoumkjlxvvacpwqtr
dbafbsyiznozmkjlxevacpwqtr
dghfbsyidnoumkjlxrvjcpwqtr
dghfbsyiznfumkjlxeqacpwqta
dghfbsfiznoumkjvxevacjwqtr
dghfbsyimnoumrjlhevacpwqtr
dghflsyiznoumkjlxevacpvqmr
dghfbmfiznoumkjlxevacpdqtr
dghfbsyizsouzkjlxevscpwqtr
dghfksyiznoumimlxevacpwqtr
dghfbsyiznoumkjlxevbwpwqur
wghcbsyiznoumkjlkevacpwqtr
kghfbioiznoumkjlxevacpwqtr
dghfbsiizeoumkjlxmvacpwqtr
dglfbsyilnoumkjlxevpcpwqtr
dgqfbsylznoumkjlxevacpwqcr
dglfhsyiznoumkjlxevacpwqdr
dghfbsymznoumkjlxbvacpwqtb
hghfbsyizhoumkjlxtvacpwqtr
dghdbsyiznoumkjlxeiacpyqtr
dohfbsyiznoumkjmxlvacpwqtr
xhhfbsyiznoumkjlxegacpwqtr
dlhfbsyiznoumkjlxnvahpwqtr
dghfbsyiznovdpjlxevacpwqtr
dgcfbsyiznoumkjlxevactwqdr
dghfksyiknoumkjlxevacpwqcr
ughfqsyiznoumkjlxevacpwctr
dghfbjyiznoumkjlxsvacnwqtr
dgwfbagiznoumkjlxevacpwqtr
dghfbsyiznoumknlxevtcpwqdr
jghfksyiznoumkjlxeoacpwqtr
dghfbsyiznoimkjlwezacpwqtr
dghfbsyiunoumkjlxeqacpwstr
dghfbsyizjoumkwlxevaypwqtr
dghfysriznoumkjlxevucpwqtr
dghfbsygzjoumkjfxevacpwqtr
dghfbhviznoumkjlxevacpwqtq
dghfbsyiznoumkjvwevacpwqur
dghfbsyiznoumtjlxevacplqnr
yghfbsysznouykjlxevacpwqtr
dgwfbsiiznoumkjlxevacfwqtr
dghfbsyizooumkjlxevampiqtr
dshfbsyiznoumkjlxevawpoqtr
dghtbsyxznuumkjlxevacpwqtr
dkhfblyiznoumkjlxevacpaqtr
dgkfbsyiinoumkjlxegacpwqtr
dghfbtxiznouhkjlxevacpwqtr
dghfbsyiznoumkjlxkvmcpeqtr
dghfbsyiznoumkjlghvacpwqmr
dghfbsbizioumkjlcevacpwqtr
dphfbsyizhoumkjwxevacpwqtr
dghfbsyiznqumkjlugvacpwqtr
dghfbsjinnoumkjlxevacpwetr
mghfbsyiznoumkjlxfvacpjqtr
dghfbsxiznoumkjlxetacwwqtr
dghmbsyiznoumbjlxevacpwqyr
dghfbsyiznwumkjlwevacmwqtr
dgkfbsyiznotmkjlxevacpwstr
dghfbsyiznouykjlxeiacuwqtr
dghfbsynznbhmkjlxevacpwqtr
dgyfbsyiznoumtjlbevacpwqtr
dghfbftiznoumkjlxevacpwatr
dghfvsyiznouikjlievacpwqtr
dghfbsyiznodmkjlxevncpwqtz
yfhfbsyiznoumkjluevacpwqtr
dghfbzyiznoumhflxevacpwqtr
dphfbsyizncumkjlxevacpwqtf
dghfasyiznoumkjlxeaicpwqtr
dgffbsyiznoumkjlzevacpwqsr
dghfbsyiznoumkmxxcvacpwqtr
dghffsyiznoumkjlxevacpwqre
dghfbsyizndmmkjlxemacpwqtr
dghfbsviznoamkjlxevappwqtr
dghfbsyiznouckrlxevacpdqtr
dgwfbsyiznyumkjlxevacpqqtr
dujfbsyiznoumgjlxevacpwqtr
dghobsailnoumkjlxevacpwqtr
dghfkqyiznoumknlxevacpwqtr
dghfbyypznoumkjlxevacpwatr
wqhfbsyiznoumkjlxevzcpwqtr
dghfbsyiznoumwjlxrvacppqtr
dghfbsymznoumkflxevacplqtr
dghfbsyiznounkjpgevacpwqtr
ighfbsyijnoumxjlxevacpwqtr
dghfbsyizroumkjllevncpwqtr
dghfbsliznokmkjlxevacpwqtb
dgefbsyiznoumkqlxevpcpwqtr
dghfbtypznouzkjlxevacpwqtr
dmhfbsyiznoumkjlxeyactwqtr
vohfbsyiznoumkjlqevacpwqtr
dgsfpsyiznodmkjlxevacpwqtr
dghfzsyijnoumkjnxevacpwqtr
dghfbayijroumkjlxevacpwqtr
dghfbsyiznodmxjlxgvacpwqtr
dghfbsyiznocmkjlxhvaipwqtr
dghmbsyignoumkjlxevacpoqtr
dghfbsyiznosmkjlncvacpwqtr
dggfbsyiznuumkjlxevacpwqrr
dghibsyilnoumkjlxevacowqtr
dghfbsyiznoumkjluevbcowqtr
dghfbsaiznyuvkjlxevacpwqtr
dgnfxsyiznommkjlxevacpwqtr
dghfbnyiznoumkjlsnvacpwqtr
dghfssiiznoumkjlxavacpwqtr
dghfbsyizneumajlxevacfwqtr
dghfbsyiznoumkjlxevycpvptr
qghfbsyizgoumkjlxevacpwttr
vghfbsyiznoumkjlievaepwqtr
dghfbsyiznoumejlxjvacpwdtr
dghfbsyispoumkjlxevacpwqtg
duhfbsyizpoumkjlxenacpwqtr
dghfbsyifnoumkblxevacpnqtr
bghfbsyxznoumkjleevacpwqtr
dgtfbsyzpnoumkjlxevacpwqtr
dghfbsyiznoumkjlsecacpwqth
dghfqsyiznjumkjlxevawpwqtr
dgcfbsyizboumkjlxevacqwqtr
dghfbqyiznoumkjkxevacpwqtj
dgyfbsyfznoumkjlievacpwqtr
dghfdsyiznoumkplxevacpwdtr
dphfbsyuznkumkjlxevacpwqtr
dghfbsyiznoupkjitevacpwqtr
dghfisyiznoamkjlxevacpwqwr
dgufbsyiznoumkjlxivvcpwqtr
dghfbvyiznoumkjlxevacvwqtz
dghfbsyiqnxumkjlxbvacpwqtr
dghubsyiznqumkflxevacpwqtr
dghfbsyiznzumkjlxevacpdbtr
dghfbsyiznoumkjlxehacpwwrr
mghfbsyiznoumkjlxevacpwqbp
dvhfbryiznoumkclxevacpwqtr
dghbbsyiznotmkjlxevacpwqhr
dghfrsyiznoomkjlxevacpwqto
dghfbkyiznoumkjlxeracpxqtr
dghfbfyizfoumkjlxevacpwjtr
dghfbsyizqoulkjlxevacpwqtt
dghfbsyiwnoumkjlxevacxwgtr
dghfbsyiznormkjlgxvacpwqtr
dghybsyizioumkjoxevacpwqtr
dchfbsyiznoumkjlxyvacpwqtc
dgyfbsyiznouckjlxewacpwqtr
dakfbsyeznoumkjlxevacpwqtr
";
    }
}