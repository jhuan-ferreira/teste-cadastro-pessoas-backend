using FluentValidation;

public static class CpfValidator
{
    public static IRuleBuilderOptions<T, string> IsCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(cpf =>
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            var cpfDigits = new string(cpf.Where(char.IsDigit).ToArray());
            if (cpfDigits.Length != 11) return false;
            if (cpfDigits.Distinct().Count() == 1) return false;

            int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int rest;
            tempCpf = cpfDigits.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = digit + rest.ToString();
            return cpfDigits.EndsWith(digit);

        }).WithMessage("CPF inválido.");
    }
}