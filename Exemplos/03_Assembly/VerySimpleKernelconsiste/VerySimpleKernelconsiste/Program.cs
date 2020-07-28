namespace VerySimpleKernelconsiste
{
    unsafe static class Program
    {
        //Configurado no Build do projeto, Permitir código não seguro
        //Assim permitiu a inclusão de * nas variáveis e conversões
        //Quando * é usado em um tipo de dados, é um ponteiro para esse tipo. 
        public static void Main()
        {
            byte* videoPositionPointer = (byte*)0xB8000;

            *videoPositionPointer = (byte)'A';
            videoPositionPointer++;
            *videoPositionPointer = 15;

            while (true) ;
        }
    }
}
