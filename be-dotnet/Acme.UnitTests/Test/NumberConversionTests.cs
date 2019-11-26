using System;
using System.Linq;
using Xunit;

namespace Acme.UnitTests.Test
{
    public class NumberConversionTests
    {
        [Fact]
        public void BuildArray_WhenCombinationIsNumberLessThan10_ReturnNumber()
        {
            string valueCombination = "9"; //"19X11/--7X";

            BollingNumberTranslatorService service = new BollingNumberTranslatorService();
            var returnableArray = service.BuildArray(valueCombination);


            Assert.Equal(9, returnableArray[0]);
        }

        [Fact]
        public void BuildArray_WhenCombinationIsX_ReturnNumber10()
        {
            string valueCombination = "X"; //"19X11/--7X";

            BollingNumberTranslatorService service = new BollingNumberTranslatorService();
            var returnableArray = service.BuildArray(valueCombination);


            Assert.Equal(10, returnableArray[0]);
        }

        [Fact]
        public void BuildArray_WhenCombinationIsHyphen_ReturnNumber0()
        {
            string valueCombination = "-"; //"19X11/--7X";

            BollingNumberTranslatorService service = new BollingNumberTranslatorService();
            var returnableArray = service.BuildArray(valueCombination);


            Assert.Equal(0, returnableArray[0]);
        }

        [Fact]
        public void BuildArray_WhenCombinationIsDash_Return10MinusPreviousNumber()
        {
            string valueCombination = "/"; //"19X11/--7X";

            BollingNumberTranslatorService service = new BollingNumberTranslatorService();
            var returnableArray = service.BuildArray(valueCombination);

            Assert.Equal(10, returnableArray[0]);

            valueCombination = "1/";
            returnableArray = service.BuildArray(valueCombination);
            Assert.Equal(9, returnableArray[1]);
        }
    }

    public class BollingNumberTranslatorService
    {
        public short[] BuildArray(string valueCombination)
        {
            short[] bollingArray = new short[10];

            for (int i = 0; i < valueCombination.Length; i++)
            {
                switch (valueCombination[i])
                {
                    case '/':
                        var previous = short.Parse(i > 0 ? valueCombination[i - 1].ToString() : "0");
                        bollingArray[i] = (short)(10 - previous);
                        break;
                    case 'X':
                        bollingArray[i] = 10;
                        break;
                    case '-':
                        bollingArray[i] = 0;
                        break;
                    default:
                        bollingArray[i] = short.Parse(valueCombination[i].ToString());
                        break;
                }
            }

            return bollingArray;
        }
    }

}
