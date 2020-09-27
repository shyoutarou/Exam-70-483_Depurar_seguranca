Welcome to the Exam-70-483_Depurar_seguranca wiki!

Resumo do treinamento para o exame.

1. [Criar_usar_tipos](https://github.com/shyoutarou/Exam-70-483_Criar_usar_tipos/wiki/Criar_usar_tipos)
     - [GitHub Page](https://shyoutarou.github.io/Exam-70-483_Criar_usar_tipos/)
2. [Gerenciar_fluxo](https://github.com/shyoutarou/Exam-70-483_Gerenciar_fluxo/wiki/Gerenciar_fluxo)
     - [GitHub Page](https://shyoutarou.github.io/Exam-70-483_Gerenciar_fluxo/)
3. [Acesso_dados](https://github.com/shyoutarou/Exam-70-483_Acesso_dados/wiki/Acesso_dados)
     - [GitHub Page](https://shyoutarou.github.io/Exam-70-483_Acesso_dados/)
4. [Depurar_segurança](https://github.com/shyoutarou/Exam-70-483_Depurar_segurança/wiki/Depurar_segurança)
     - [GitHub Page](https://shyoutarou.github.io/Exam-70-483_Depurar_segurança/)
5. [Csharp8_Csharp9](https://github.com/shyoutarou/Exam-70-483_Csharp8_Csharp9/wiki/Csharp8_Csharp9)
     - [GitHub Page](https://shyoutarou.github.io/Exam-70-483_Csharp8_Csharp9/)
6. [Questions](https://github.com/shyoutarou/Exam-70-483_Questions/wiki/Questions)


DEPURAR APLICATIVOS E SEGURANÇA (25–30%)
Validar entrada de aplicativo 
•	Validar dados JSON; escolher o tipo de coleta de dados adequado; gerenciar integridade de dados; avaliar uma expressão regular para validar o formato de entrada; usar funções internas para validar tipos e conteúdos

Quando seu aplicativo está em produção, ele precisa lidar com vários tipos de entrada. Parte dessa entrada vem de outros sistemas com os quais se integra e a maior parte é gerada pelos usuários. Esses usuários podem se enquadrar em duas categorias:
•	Usuários inocentes: são os que tentam usar seu aplicativo para realizar algum trabalho. Eles não têm más intenções ao trabalhar com seu aplicativo, mas ainda podem cometer erros. Talvez eles se esqueçam de inserir alguns dados necessários ou que digitam um erro de digitação e insiram dados inválidos.
•	Usuários maliciosos: são uma espécie diferente. Eles buscam ativamente pontos fracos em seu aplicativo e tentam explorá-los. Talvez eles queiram acessar algumas informações privilegiadas ou tentem adicionar ou remover informações. Esses são os usuários que tentam inserir dados inválidos, descompilar seu código para ver como ele funciona ou simplesmente começar a procurar áreas ocultas do seu sistema.

Mesmo quando seu aplicativo se integra a outros aplicativos que não têm más intenções, você ainda precisa validar os dados que está consumindo. Talvez você tenha desenvolvido seu aplicativo, testado e verificado e estava funcionando, mas de repente o outro sistema foi atualizado para uma nova versão. Os campos que você esperava desapareceram ou foram movidos para outro local, e novos dados são adicionados de repente. Se você não se proteger contra esse tipo de situação, eles podem travar seu aplicativo ou corromper os dados em seu sistema.

Ao criar aplicativos do mundo real, você provavelmente usará estruturas como o Windows Presentation Foundation (WPF), ASP .NET ou Entity Framework. Essas estruturas têm funcionalidade incorporada para validar dados, mas ainda é importante garantir que você saiba como executar sua própria validação.

Evitando validação

Antes de discutir maneiras de validar dados, vale a pena dedicar um momento para discutir maneiras de evitar a validação. Se o usuário não puder inserir um valor incorreto, não será necessário escrever um código para validar o valor. Os programas C# podem usar muitos tipos diferentes de controles e muitos permitem restringir a entrada do usuário a valores válidos.

Por exemplo, suponha que o programa precise que o usuário insira um valor inteiro entre 1 e 10. Você pode permitir que o usuário digite o valor em um TextBox. Nesse caso, o código precisaria verificar o valor para garantir que o usuário não inserisse 100, 0, –8 ou dez. No entanto, se o programa fizer com que o usuário selecione o valor em um TrackBar, o usuário não poderá selecionar um valor inválido. Você pode definir as propriedades Mínima e Máxima do TrackBar e o controle faz toda a validação funcionar para você. Isso não apenas poupa o trabalho de escrever e depurar o código de validação, mas também o de descobrir o que fazer se o usuário digitar um número inválido. Você não precisa exibir uma caixa de mensagem para o usuário, e o fluxo de trabalho do usuário não é interrompido pela caixa de mensagem.

Muitos controles permitem ao usuário selecionar valores de tipos específicos, como cores, datas, arquivos, pastas, fontes, números, um único item de uma lista e vários itens de uma lista. Sempre que você cria um programa e planeja permitir que o usuário digite algo em um TextBox, você deve se perguntar se existe algum controle que permita ao usuário selecionar o valor em vez de digitá-lo.

A lista a seguir resume os três estágios da validação de entrada, variando de mais freqüente e menos invasivo a menos frequente e mais invasivo:
1.	Validação de pressionamento de tecla: o programa pode ignorar qualquer pressionamento de tecla que não faça sentido, mas certifique-se de permitir valores que possam se transformar em algo que faça sentido como "-". pode se transformar em "–123". Opcionalmente, você pode marcar o campo como contendo um valor inválido, desde que não interrompa o usuário.
2.	Validação de campo: quando o foco sai de um campo, o programa pode validar seu conteúdo e sinalizar valores inválidos. Agora, se o campo contiver "-". é inválido. O programa deve exibir um indicador de que o valor é inválido, mas não deve forçar o usuário a corrigi-lo ainda.
3.	Validação do formulário: quando o usuário tenta enviar os valores do formulário, o programa deve validar todos os valores e exibir mensagens de erro, se apropriado. Este é o único local em que o programa deve forçar o usuário a fixar valores. A validação de formulário também é o único local em que o programa pode executar validações entre campos em que o valor de um campo depende dos valores em outros campos. Por exemplo, o CEP 02138 está em Cambridge, MA. Se o usuário digitar esse CEP, mas o estado AZ, algo está errado. O CEP está incorreto ou o estado está incorreto (ou ambos).

Gerenciando a integridade dos dados

Vamos supor que um usuário comete um erro de digitação em uma data. O aplicativo ao executar cálculos na data, lança uma exceção e pare de funcionar, obrigando o usuário a redigitar todos os campos de um formulário. Pior seria se os dados inválidos não forem reconhecidos e salvos no seu banco de dados comprometendo a integridade dos dados.

Outra situação seria o caso de um aplicativo de compras online. Um dos administradores decide fazer uma limpeza e remove algumas contas de usuário que ele presume que não estão mais em uso. Porém, essas contas têm um histórico de compras, e o seu banco de dados fica com pedidos que não podem mais ser vinculados a um usuário específico.Novamente, os dados estão em um estado inconsistente.

Outra situação pode surgir em um aplicativo para um banco. De repente, há uma queda de energia ou uma falha de hardware, no meio de uma transferência bancária e certa quantia em dinheiro foi removida de uma conta e não foi adicionada a outra. Ou seja, o dinheiro some. Evitar esses tipos de problemas é a área de gerenciamento da integridade dos dados. Existem quatro tipos diferentes de integridade de dados:
1.	Integridade da entidade: Declara que cada entidade (um registro em um banco de dados) deve ser identificável exclusivamente. Em um banco de dados, isso é alcançado usando uma coluna de chave primária, que identifica exclusivamente cada linha de dados. Pode ser gerado pelo banco de dados ou pelo seu aplicativo.
2.	Integridade do domínio: Refere-se à validade dos dados que uma entidade contém. Pode ser sobre o tipo de dados e os possíveis valores permitidos (um código postal válido, um número dentro de um determinado intervalo ou um valor padrão, por exemplo).
3.	Integridade referencial: O relacionamento que as entidades mantêm entre si, como o relacionamento entre um pedido e um cliente.
4.	Integridade definida pelo usuário: Compreende regras de negócios específicas que você precisa aplicar. Uma regra comercial para uma loja virtual pode envolver um novo cliente que não tem permissão para fazer um pedido acima de um determinado valor em dólar. 

Para os problemas acima apreentados, a maioria dessas verificações de integridade é integrada aos sistemas modernos de banco de dados. Por exemplo, ao trabalhar com o modelo de banco de dados relacional, você provavelmente usará um mapeador objeto-relacional (ORM), como o Entity Framework, que permite várias maneiras diferentes de trabalhar com seu banco de dados. Uma abordagem é definir seu modelo de objeto no código e, em seguida, permitir que o Entity Framework gere um banco de dados que possa armazenar seu modelo. Você pode anotar suas classes com atributos que especificam certas regras de validação ou pode usar uma sintaxe de mapeamento especial para configurar a maneira como o esquema do banco de dados é gerado.

Você pode usar chaves primárias e estrangeiras para permitir que o banco de dados realize determinadas verificações básicas em seus dados. Uma chave primária identifica exclusivamente cada linha de dados, garantindo a integridade de entidade. Uma chave estrangeira é usada para apontar para outro registro (como no exemplo, uma pessoa associada a um histórico de pedidos). Você pode configurar o banco de dados para impedir a remoção de uma pessoa sem antes remover o histórico de pedidos, garantindo integridade referencial. 

A integridade do domínio dos tipos de dados pode ser conseguida aplicando a suas propriedades da classe os atributos especiais encontrados na biblioteca System.ComponentModel.DataAnnotations.dll. Por exemplo, ao trabalhar com uma loja virtual, você precisará criar classes para pedido, cliente, linha de pedido e produto. Criaríamos as classes Customer e Address aplicando a suas propriedades da classe os atributos especiais do DataAnnotations., conforme abaixo:

public class Customer
{
    public int Id { get; set; }
    [Required, MaxLength(20)]
    public string FirstName { get; set; }
    [Required, MaxLength(20)]
    public string LastName { get; set; }
    [Required]
    public Address ShippingAddress { get; set; }
    [Required] public Address BillingAddress { get; set; }
}

public class Address
{
    public int Id { get; set; }
    [Required, MaxLength(20)]
    public string AddressLine1 { get; set; }
    [Required, MaxLength(20)]
    public string AddressLine2 { get; set; }
    [Required, MaxLength(20)]
    public string City { get; set; }
    [RegularExpression(@"^[1 - 9][0 - 9]{3}\s?[a - zA - Z]{2}$")]
    public string ZipCode { get; set; }
}

Repare que ao adicionar o atributo Required determina que esses campos são obrigatórios, como o nome do cliente e a quantidade de produtos que você deseja solicitar. Outros atributos como MaxLength e RegularExpression, podem ser utilizados para restringir o limite ou formato de entrada de um campo. Quando salvar as alterações no banco de dados, o código de validação será executado, garantindo assim a integridade do domínio no seu aplicativo.

Você pode usar os seguintes atributos predefinidos:
DataTypeAttribute	RangeAttribute	RegularExpressionAttribute	RequiredAttribute
StringLengthAttribute	CustomValidationAttribute	MaxLengthAttribute	MinLengthAttribute

	Apesar de geralmente encontrarmos exemplos de código utilizando o DataAnnotations em projetos MVC o WinForms pois teoricamente precisamos de uma fonte se dados e necessariamente precisamos de um objeto Context que faz a abstração dessa fonte de dados. É possível criar um aplicativo de console criando manualmente um ValidationContext como mostra o exemplo abaixo:

var customer = new Customer();
customer.FirstName = "João";
customer.LastName = "1234567891012345678910";
var context = new ValidationContext(customer, serviceProvider: null, items: null);
var results = new List<ValidationResult>();

var isValid = Validator.TryValidateObject(customer, context, results, true);

if (!isValid)
{
    foreach (var validationResult in results)
    {
        Console.WriteLine(validationResult.ErrorMessage);
    }
}

 

Uma coisa a se observar é que se você precisar de validação para todas as propriedades, precisará passar true para o último parâmetro do método TryValidateObject. Abaixo um exemplo de código em um projeto MVC que inspeciona o funcionamento do DataAnnotations aplicado as classes Customer e Address, anteriormente criadas.

public class ShopContext : DbContext
{
    static ShopContext()
    {
        Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ShopContext>());
    }

    public IDbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
        .HasRequired(bm => bm.BillingAddress)
        .WithMany().WillCascadeOnDelete(false);

        using (ShopContext ctx = new ShopContext())
        {
            Address a = new Address
            {
                //AddressLine1 = "Somewhere 1",
                //AddressLine2 = "At some floor",
                ////City = "SomeCity",
                //ZipCode = "1111AA"
            };

            GenericValidator<Address>.Validate(a);

            Customer c = new Customer()
            {
                //FirstName = "John",
                LastName = "Doe",
                BillingAddress = a,
                ShippingAddress = a,
            };

            GenericValidator<Customer>.Validate(c);

            ctx.Customers.Add(c);
            ctx.SaveChanges();
        }

    }
}

public static class GenericValidator<T>
{
    public static IList<ValidationResult> Validate(T entity)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(entity, null, null);
        Validator.TryValidateObject(entity, context, results);
        return results;
    }
}

Ao executar esse código de validação manualmente fora do contexto do Entity Framework em modo Debug, ao passar pelo método GenericValidator podemos observar os erros relatados.
 

Os modelos baseados em Code First usam cadeias de conexão ADO.NET normais. Por exemplo:

  <connectionStrings>
    <add name="ShopDBContext" connectionString="data source=localhost;initial catalog=School;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
  </connectionStrings>

	E no arquivo Model ficaria:

public class ShopContext : DbContext
{
    static ShopContext() : base("name=ShopDBContext ")
    {
        Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ShopContext>());
    }

    public IDbSet<Customer> Customers { get; set; }

    //............
}

Uma alternativa para definir o inicializador no arquivo Web.config em projetos MVC é fazê-lo no código, adicionando uma instrução Database.SetInitializer ao método Application_Start no arquivo Global.asax.cs.

protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);

    Database.SetInitializer(new CreateDatabaseIfNotExists<ShopContext>());
}

A integridade definida pelo usuário seria o maior problema, uma vez que isso não pode ser tratado automaticamente pelo Entity Framework. Você pode definir essas verificações em controles na interface do usuário ou escrever um código personalizado que será executado pelo seu banco de dados. Uma maneira é usar um trigger. Os triggers são métodos especiais que são executados quando os dados do seu banco de dados são atualizados, inseridos ou removidos. Essa ação aciona seu método para executar. Outra maneira seria usar procedimentos armazenados (Store Procedures), que são sub-rotinas armazenadas em seu banco de dados e podem ser executadas para validar a data ou controlar o acesso aos dados.

Restrições de banco de dados

Restrições de banco de dados são regras de integridade declarativas da definição de estruturas de tabela. Eles incluem os seguintes 7 tipos de restrições:
1)	De tipo de dados: Isso define o tipo de dados, o comprimento dos dados e alguns outros atributos que estão especificamente associados ao tipo de dados em uma coluna.
2)	De padrão: Isso define qual valor a coluna deve usar quando nenhum valor foi fornecido explicitamente ao inserir um registro na tabela.
3)	De anulabilidade: Isso define que, se uma coluna NÃO for NULL ou permitir que valores NULL sejam armazenados nela.
4)	De chave primária: Este é o identificador exclusivo da tabela. Cada linha deve ter um valor distinto. A chave primária pode ser um número inteiro incrementalmente sequencial ou uma seleção natural de dados que representa o que está acontecendo no mundo real (por exemplo, Número do Seguro Social). Valores NULL não são permitidos em valores de chave primária.
5)	De unicidade: Isso define que os valores em uma coluna devem ser exclusivos e nenhuma duplicata deve ser armazenada. Às vezes, os dados em uma coluna devem ser exclusivos, mesmo que a coluna não atue como chave primária da tabela. Por exemplo, a coluna CategoryName é exclusiva na tabela de categorias, mas CategoryName não é a chave primária da tabela.
6)	De chave estrangeira: Isso define como a integridade referencial é imposta entre duas tabelas.
7)	De verificação: Isso define uma regra de validação para os valores dos dados em uma coluna, portanto, é uma restrição de integridade de dados definida pelo usuário. Esta regra é definida pelo usuário ao projetar a coluna em uma tabela. Nem todo mecanismo de banco de dados suporta restrições de verificação. A partir da versão 5.0, o MySQL não suporta restrição de verificação. Mas você pode usar o tipo de dados enum ou definir o tipo de dados para obter algumas de suas funcionalidades disponíveis em outros sistemas de gerenciamento de banco de dados relacional (Oracle, SQL Server, etc.).
Tipo de integridade de dados	Imposto por banco de dados
Integridade de entidade/linha	Restrição de chave primária
Restrição de unicidade
Integridade de domínio/coluna	Restrição de chave estrangeira
Restrição de verificação
Restrição padrão
Restrição de tipo de dados
Restrição de anulabilidade
Integridade referencial	Restrição de chave estrangeira
Integridade definida pelo usuário	Restrição de verificação

Trigger (Transact-SQL)

Um gatilho é um tipo especial de procedimento armazenado executado automaticamente quando um evento ocorre no servidor de banco de dados. No Transact-SQL existem 3 tipos:
1)	Os gatilhos DML são executados quando um usuário tenta modificar dados por meio de um evento DML (linguagem de manipulação de dados). Os eventos DML são instruções INSERT, UPDATE ou DELETE em uma tabela ou exibição. Esses gatilhos são disparados quando qualquer evento válido é acionado, sejam as linhas da tabela afetadas ou não. 

SQL Server Syntax  
-- Trigger on an INSERT, UPDATE, or DELETE statement to a 
-- table (DML Trigger on memory-optimized tables)  

CREATE [ OR ALTER ] TRIGGER [ schema_name . ]trigger_name   
ON { table }   
[ WITH <dml_trigger_option> [ ,...n ] ]  
{ FOR | AFTER }   
{ [ INSERT ] [ , ] [ UPDATE ] [ , ] [ DELETE ] }   
AS { sql_statement  [ ; ] [ ,...n ] }  

<dml_trigger_option> ::=  
    [ NATIVE_COMPILATION ]  
    [ SCHEMABINDING ]  
    [ EXECUTE AS Clause ]  

2)	Os gatilhos DDL são executados em resposta a diversos eventos DDL (linguagem de definição de dados). Esses eventos correspondem, basicamente, a instruções Transact-SQL CREATE, ALTER e DROP e determinados procedimentos armazenados do sistema que executam operações do tipo DDL.

SQL Server Syntax  
-- Trigger on a CREATE, ALTER, DROP, GRANT, DENY, 
-- REVOKE or UPDATE statement (DDL Trigger)  

CREATE [ OR ALTER ] TRIGGER trigger_name   
ON { ALL SERVER | DATABASE }   
[ WITH <ddl_trigger_option> [ ,...n ] ]  
{ FOR | AFTER } { event_type | event_group } [ ,...n ]  
AS { sql_statement  [ ; ] [ ,...n ] | EXTERNAL NAME < method specifier >  [ ; ] }  

<ddl_trigger_option> ::=  
    [ ENCRYPTION ]  
    [ EXECUTE AS Clause ]  

3)	Os gatilhos de logon são disparados em resposta ao evento LOGON gerado quando a sessão de um usuário está sendo estabelecida. Crie gatilhos diretamente de instruções Transact-SQL ou de métodos de assemblies criados no CLR (Common Language Runtime) do Microsoft .NET Framework e carregados em uma instância do SQL Server. O SQL Server permite criar vários gatilhos para qualquer instrução específica.

SQL Server Syntax  
CREATE [ OR ALTER ] TRIGGER trigger_name   
ON ALL SERVER   
[ WITH <logon_trigger_option> [ ,...n ] ]  
{ FOR| AFTER } LOGON    
AS { sql_statement  [ ; ] [ ,...n ] | EXTERNAL NAME < method specifier >  [ ; ] }  

<logon_trigger_option> ::=  
    [ ENCRYPTION ]  
    [ EXECUTE AS Clause ]  

Store Procedures

Cria um procedimento armazenado CLR (Common Language Runtime) ou Transact-SQL no SQL Server, no Banco de Dados SQL do Azure, no SQL Data Warehouse do Azure e no Parallel Data Warehouse. Procedimentos armazenados são semelhantes a procedimentos em outras linguagens de programação no sentido de que podem:
•	Aceitar parâmetros de entrada e retornar vários valores no formulário de parâmetros de saída para o procedimento de chamada ou lote.
•	Conter instruções de programação que executam operações no banco de dados, inclusive chamar outros procedimentos.
•	Retornar um valor de status a um procedimento de chamada ou lote para indicar êxito ou falha (e o motivo da falha).

Use esta instrução para criar um procedimento permanente no banco de dados

-- Transact-SQL Syntax for Stored Procedures in SQL Server and Azure SQL Database  

CREATE [ OR ALTER ] { PROC | PROCEDURE } 
    [schema_name.] procedure_name [ ; number ]   
    [ { @parameter [ type_schema_name. ] data_type }  
        [ VARYING ] [ = default ] [ OUT | OUTPUT | [READONLY]  
    ] [ ,...n ]   
[ WITH <procedure_option> [ ,...n ] ]  
[ FOR REPLICATION ]   
AS { [ BEGIN ] sql_statement [;] [ ...n ] [ END ] }  
[;]  

<procedure_option> ::=   
    [ ENCRYPTION ]  
    [ RECOMPILE ]  
    [ EXECUTE AS Clause ] 

Embora esta não seja uma lista completa de práticas recomendadas, estas sugestões podem melhorar o desempenho do procedimento.
•	Use a instrução SET NOCOUNT ON como a primeira instrução no corpo do procedimento. Ou seja, coloque-a logo após a palavra-chave AS. Isso desativa as mensagens que o SQL Server envia ao cliente após a execução de qualquer instrução SELECT, INSERT, UPDATE, MERGE e DELETE. Isso mantém a saída gerada a um mínimo para maior clareza. No entanto, não há nenhum benefício de desempenho mensurável no hardware de hoje. Para obter informações, veja SET NOCOUNT (Transact-SQL).
•	Use nomes de esquemas ao criar ou referenciar objetos de banco de dados no procedimento. Será necessário menos tempo de processamento para o Mecanismo de Banco de Dados resolver nomes de objetos se ele não precisar pesquisar vários esquemas. Além disso, evita problemas de acesso e permissão causados pelo esquema padrão de um usuário ser atribuído quando são criados objetos sem especificar o esquema.
•	Evite ajustar funções ao redor de colunas especificadas nas cláusulas WHERE e JOIN. Isso torna as colunas não determinísticas e impede o processador de consultas de usar índices.
•	Evite usar funções escalares em instruções SELECT que retornam muitas linhas de dados. Como a função escalar deve ser se aplicada a cada linha, o comportamento resultante é como o processamento baseado em linha e afeta o desempenho.
•	Evite o uso de SELECT *. Em vez disso, especifique os nomes de colunas necessários. Isso pode evitar alguns erros do Mecanismo de Banco de Dados que param a execução do procedimento. Por exemplo, uma instrução SELECT * que retorna dados de uma tabela de 12 colunas e, em seguida, insere os dados em uma tabela temporária de 12 colunas terá êxito até o número ou a ordem das colunas mudar em qualquer uma das tabelas.
•	Evite processar ou retornar dados em excesso. Delimite os resultados o quanto antes no código do procedimento, para que quaisquer operações subsequentes executadas pelo procedimento sejam efetuadas com o menor conjunto de dados possível. Envie apenas os dados essenciais ao aplicativo cliente. Além disso, enviar somente os dados essenciais ao aplicativo cliente é mais eficiente do que enviar dados adicionais pela rede e forçar o aplicativo cliente a trabalhar com conjuntos de resultados desnecessariamente grandes.
•	Utilize transações explícitas usando BEGIN/COMMIT TRANSACTION e mantenha as transações o mais curtas possível. Transações maiores indicam bloqueio de registro mais longo e um maior potencial para deadlock.
•	Use o recurso TRY...CATCH do Transact-SQL para tratamento de erro em um procedimento. TRY...CATCH pode encapsular um bloco inteiro de instruções Transact-SQL. Isso cria menos sobrecarga de desempenho e também torna o relatório de erros mais preciso com muito menos programação.
•	Use a palavra-chave DEFAULT em todas as colunas de tabela que sejam referenciadas pelas instruções Transact-SQL CREATE TABLE ou ALTER TABLE no corpo do procedimento. Isso impede a passagem de NULL para colunas que não permitam valores nulos.
•	Use NULL ou NOT NULL para cada coluna em uma tabela temporária. As opções ANSI_DFLT_ON e ANSI_DFLT_OFF controlam a forma como o Mecanismo de Banco de Dados atribui os atributos NULL ou NOT NULL a colunas quando esses atributos não são especificados em uma instrução CREATE TABLE ou ALTER TABLE. Se uma conexão executar um procedimento com configurações para essas opções diferentes da conexão que criou o procedimento, as colunas da tabela criada para a segunda conexão poderão ter nulidades diferentes e exibir um comportamento diferente. Se NULL ou NOT NULL for declarado explicitamente para cada coluna, as tabelas temporárias serão criadas com a mesma nulidade para todas as conexões que executam o procedimento.
•	Use instruções de modificação que convertam nulos e inclua uma lógica que elimine linhas com valores nulos das consultas. Saiba que, em Transact-SQL, NULL não significa um valor vazio ou "nada". É um espaço reservado para um valor desconhecido e pode causar um comportamento inesperado, especialmente ao consultar conjuntos de resultados ou usar funções AGGREGATE.
•	Use o operador UNION ALL em vez dos operadores UNION ou OR, a menos que haja uma necessidade específica de valores distintos. O operador UNION ALL requer menos sobrecarga de processamento, pois as duplicatas não são filtradas do conjunto de resultados.

Os exemplos desta seção demonstram a funcionalidade básica da instrução CREATE PROCEDURE por meio da sintaxe mínima necessária.SELECT DB_NAME() AS ThisDB; retorna o nome do banco de dados atual. Você pode encapsular essa instrução em um procedimento armazenado, como:

CREATE PROC What_DB_is_that @ID int   
AS    
SELECT DB_NAME(@ID) AS ThatDB;

Chame o procedimento de armazenamento com a instrução: EXEC What_DB_is_this;Um pouco mais complexo é fornecer um parâmetro de entrada para tornar o procedimento mais flexível. Por exemplo:

CREATE PROC What_DB_is_that @ID int   
AS    
SELECT DB_NAME(@ID) AS ThatDB;   

Forneça um número de ID do banco de dados quando chamar o procedimento. Por exemplo, EXEC What_DB_is_that 2; retorna tempdb.

CREATE PROCEDURE HumanResources.uspGetAllEmployees  
AS  
    SET NOCOUNT ON;  
    SELECT LastName, FirstName, JobTitle, Department  
    FROM HumanResources.vEmployeeDepartment;  
GO  

SELECT * FROM HumanResources.vEmployeeDepartment;  

O procedimento uspGetEmployees pode ser executado das seguintes maneiras:

EXECUTE HumanResources.uspGetAllEmployees;  
GO  
-- Or  
EXEC HumanResources.uspGetAllEmployees;  
GO  
-- Or, if this procedure is the first statement within a batch:  
HumanResources.uspGetAllEmployees;  

O exemplo a seguir usa o constructo TRY...CATCH para retornar informações de erros obtidos durante a execução de um procedimento armazenado.

CREATE PROCEDURE Production.uspDeleteWorkOrder ( @WorkOrderID int )  
AS  
SET NOCOUNT ON;  
BEGIN TRY  
   BEGIN TRANSACTION   
   -- Delete rows from the child table, WorkOrderRouting, for the specified work order.  
   DELETE FROM Production.WorkOrderRouting  
   WHERE WorkOrderID = @WorkOrderID;  

   -- Delete the rows from the parent table, WorkOrder, for the specified work order.  
   DELETE FROM Production.WorkOrder  
   WHERE WorkOrderID = @WorkOrderID;  

   COMMIT  

END TRY  
BEGIN CATCH  
  -- Determine if an error occurred.  
  IF @@TRANCOUNT > 0  
     ROLLBACK  

  -- Return the error information.  
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;  
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();  
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);  
END CATCH;  

GO  
EXEC Production.uspDeleteWorkOrder 13;  

/* Intentionally generate an error by reversing the order in which rows 
   are deleted from the parent and child tables. This change does not 
   cause an error when the procedure definition is altered, but produces 
   an error when the procedure is executed.  
*/  
ALTER PROCEDURE Production.uspDeleteWorkOrder ( @WorkOrderID int )  
AS  

BEGIN TRY  
   BEGIN TRANSACTION   
      -- Delete the rows from the parent table, WorkOrder, for the specified work order.  
   DELETE FROM Production.WorkOrder  
   WHERE WorkOrderID = @WorkOrderID;  

   -- Delete rows from the child table, WorkOrderRouting, for the specified work order.  
   DELETE FROM Production.WorkOrderRouting  
   WHERE WorkOrderID = @WorkOrderID;  

   COMMIT TRANSACTION  

END TRY  
BEGIN CATCH  
  -- Determine if an error occurred.  
  IF @@TRANCOUNT > 0  
     ROLLBACK TRANSACTION  

  -- Return the error information.  
  DECLARE @ErrorMessage nvarchar(4000),  @ErrorSeverity int;  
  SELECT @ErrorMessage = ERROR_MESSAGE(),@ErrorSeverity = ERROR_SEVERITY();  
  RAISERROR(@ErrorMessage, @ErrorSeverity, 1);  
END CATCH;  
GO  
-- Execute the altered procedure.  
EXEC Production.uspDeleteWorkOrder 15;  

DROP PROCEDURE Production.uspDeleteWorkOrder;  

Transaction

Outro tópico importante ao gerenciar a integridade dos dados com um banco de dados é usar transações. Uma transação ajuda a agrupar um conjunto de operações relacionadas em um banco de dados. A transação é o processo total de consulta ou manipulação do banco de dados. É uma forma de estabelecer que algo deva ser feito atomicamente, ou seja, ou faz tudo ou faz nada, não pode fazer pela metade. É tudo feito em uma viagem da aplicação para o banco de dados. Em condições normais enquanto a transação não termina outras transações não podem ver o que esta está fazendo. 

Você também pode ter problemas quando seus usuários trabalham simultaneamente com o mesmo conjunto de dados. Usando transações, você pode configurar seu banco de dados para lançar uma exceção quando houver uma atualização conflitante. No seu aplicativo, você pode capturar essas exceções e escrever código que lida com o conflito. Você pode, por exemplo, permitir que o usuário escolha qual atualização deve vencer ou pode deixar a última atualização. Isso ajuda a manter a integridade dos dados.

A maioria dos comandos SQL são transacionais implicitamente, ou seja, ele por si só já é uma transação. Você só precisa usar esses comandos (BEGIN TRANSACTION, COMMIT, ROLLBACK) explicitamente somente quando precisa usar múltiplos comandos e todos esses devam rodar atomicamente. Ou seja, eles funcionam como as chaves de um código, eles criam um bloco. Na verdade, está mais para o using do C# já que há uma consequência garantida no final da execução.

CREATE TABLE ValueTable (id int);  
BEGIN TRANSACTION;              -- aqui começa a transação
       INSERT INTO ValueTable VALUES(1);  
       INSERT INTO ValueTable VALUES(2);  
COMMIT;                         -- aqui termina e "grava" tudo

Enquanto não dá o COMMIT essas inserções não constam de fato no banco de dados. Um ROLLBACK descartaria tudo feito.Ao contrário do que muita gente acredita ROLLBACK no contexto de banco de dados não significa reverter e sim voltar ao estado original. Um processo de reversão seria de complicadíssimo à impossível. Um processo de descarte é simples e pode ser atômico.

Vamos explicar cada uma na prática. Para isso, em um banco de dados qualquer, crie a seguinte tabela:

CREATE TABLE OBJETO (
    ID INT NOT NULL PRIMARY KEY,
    COLUNA1 VARCHAR(20) NOT NULL,
    COLUNA2 VARCHAR(20) NULL);

Com a tabela criada, vamos às transações.O SQL Server opera nos seguintes modos de transação:
1)	Transações de confirmação automática (autoconfirmação):Cada instrução individual é uma transação.
Por ezemplo, ao tentarmos inserir null na COLUNA1 da tabela.

INSERT INTO OBJETO VALUES (2, null, 'Segunda coluna');

A instrução produz um erro e a instrução é automaticamente revertida. Isso significa que o SQL Server usa transações deautoconfirmação, ou seja, cada instrução é uma transação por si só.

2)	Transações explícitas:Cada transação é iniciada e finalizada explicitamente, o SQL utiliza 3 comandos para controlar isso.
•	BEGIN TRANSACTION indica onde ela deve começar, então os comandos SQL a seguir estarão dentro desta transação.
•	COMMIT TRANSACTION indica o fim normal da transação, o que tiver de comando depois já não fará parte desta transação. Neste momento tudo o que foi manipulado passa fazer parte do banco de dados normalmente e operações diversas passam enxergar o que foi feito.
•	ROLLBACK TRANSACTION também fecha o bloco da transação e é a indicação que a transação deve ser terminada, mas tudo que tentou ser feito deve ser descartado porque alguma coisa errada aconteceu e ela não pode terminar normalmente. Nada realizado dentro dela será perdurado no banco de dados.
A palavra TRANSACTION pode ser abreviada para TRAN.

BEGIN TRAN

INSERT INTO OBJETO VALUES (1, 'Primeira coluna', 'Primeira coluna');
INSERT INTO OBJETO VALUES (2, null, 'Segunda coluna');

if @@ERROR <> 0
ROLLBACK TRAN;
else
COMMIT TRAN;

Ou utilizando Try-Catch:

BEGIN TRY
    BEGIN TRAN
    INSERT INTO OBJETO VALUES (1, 'Primeira coluna', 'Primeira coluna');
    INSERT INTO OBJETO VALUES (2, null, 'Segunda coluna');
    COMMIT TRAN;
END TRY

BEGIN CATCH
    SELECT ERROR_NUMBER() AS "ERROR_NUMBER",
           ERROR_SEVERITY() AS "ERROR_SEVERITY",
           ERROR_STATE() AS "ERROR_STATE",
           ERROR_PROCEDURE() AS "ERROR_PROCEDURE",
           ERROR_LINE() AS "ERROR_LINE",
           ERROR_MESSAGE() AS "ERROR_MESSAGE"

    RAISERROR('Erro na transação', 14, 1)
    ROLLBACK TRAN;
END CATCH;

3)	Transações implícitas: Uma transação nova é iniciada implicitamente quando a transação anterior é concluída, mas cada transação é explicitamente concluída com uma instrução COMMIT ou ROLLBACK.
Para usar esse tipo de transação, precisamos ativá-la no SQL Server usando o código abaixo:

SET IMPLICIT_TRANSACTIONS ON;

Agora execute a instrução abaixo para verificar quantas transações em aberto existem:

SELECT @@TRANCOUNT

Você deve ter como resultado o valor 0. Crie uma tabela simples e execute o @@TRANCOUNT novamente:

CREATE TABLE TESTE (ID INT PRIMARY KEY);
SELECT @@TRANCOUNT;

O resultado agora é 1, uma transação foi inicializada. E se executamos um insert simples nessa tabela?

INSERT INTO TESTE VALUES (5);
SELECT * FROM TESTE;
SELECT @@TRANCOUNT;

A instrução insert foi executada, porém continuamos a ter uma única transação. Para finalizar, vamos executar um ROLLBACK:

ROLLBACK TRAN;
SELECT @@TRANCOUNT;

Pronto, nossa transação foi revertida. Se você quiser confirmar, execute um SELECT na tabela TESTE. Aparece uma mensagem de erro informando que a tabela não existe é exibida, ou seja, nosso ROLLBACK reverteu tudo, desde o insert à criação da tabela.Para finalizar, vamos desativar as transações implícitas:

SET IMPLICIT_TRANSACTIONS OFF;

4)	Transações de escopo de lote:Aplicável apenas a MARS (Conjuntos de Resultados Ativos Múltiplos), a transação Transact-SQL explícita ou implícita iniciada em uma sessão MARS se torna uma transação de escopo de lote. A transação de escopo de lote que não é confirmada ou revertida quando um lote é concluído, será revertida automaticamente pelo SQL Server.

Fazendo afirmações (Assertions)

Outra precaução que você pode tomar para gerenciar a integridade dos dados à medida que eles passam pelo sistema é usar asserção. Uma asserção é um pedaço de código que faz uma afirmação específica sobre os dados e que interrompe a execução se essa afirmação for falsa. A classe System.Diagnostics.Debug fornece um método Assert que geralmente é usado para validar dados. Este método usa um valor booleano como seu primeiro parâmetro e lança uma exceção se esse valor for falso. Outros parâmetros permitem especificar outras mensagens a serem exibidas para fornecer mais informações sobre onde a asserção falhou.

Em uma compilação de depuração, o método Assert interrompe a execução e exibe um rastreamento de pilha. Em uma compilação de versão, o programa ignora a chamada para Assert, continuando em execução mesmo se a asserção for falsa.

Um método pode usar Assert para verificar se seus dados fazem sentido. Por exemplo, suponha que um método pode começar com a seguinte declaração Assert para verificar se uma matriz de itens não contém mais de 100 itens:

Debug.Assert(items.Length <= 100)

Se um pedido contiver mais de 100 itens, a instrução Assert interromperá a execução, para que você possa examinar o código para decidir se isso é um bug ou apenas um pedido incomum. 
 

O diálogo mostrado acima é uma grande diferença entre lançar suas próprias exceções e usando a declaração Assert. Outra grande diferença é que a instrução Assert é executada apenas nas versões de depuração de um programa. Nas compilações de versão(Release), a instrução Assert é completamente ignorada. Se você clicar em Abortar, o programa será encerrado. Se você clicar em Repetir, o Visual Studio interromperá a execução do programa
na declaração Assert, para tentar descobrir o que deu errado. Se você clicar em Ignorar, o programa continua executando após a declaração Assert.

Versões sobrecarregadas do método Assert permitem indicar uma mensagem que a caixa de diálogo deve exibir além do rastreamento da pilha. Você pode usar asserções em qualquer lugar do programa em que ache que os dados possam ser corrompidos. Um local particularmente bom para afirmações é no início de qualquer método que use os dados. Se um dado for inválido e o programa não puder continuar, lance uma exceção. Se um dado é incomum e pode indicar um erro, mas o programa pode continuar significativamente, use Debug.Assert para detectar o valor incomum durante o teste.

Outro bom lugar para afirmações é no final de qualquer método que manipula os dados. Em que aponte o código pode verificar se as alterações feitas pelo método fazem sentido. Às vezes, os programadores resistem em colocar o código de validação de dados no final de seus métodos porque não conseguem visualizar o código cometendo um erro. Isso é natural porque eles acabaram de escrever o código e, se ele contivesse um erro, eles o teriam corrigido. Porém, como as asserções são ignoradas nas versões, o desempenho do programa não sofre, mesmo que você adiciona muitas asserções a um método. Mesmo que as validações nunca detectem um erro, pelo menos você terá alguns motivos para acreditar que o código está correto. 

Métodos Parse, TryParse (veja mais detalhes no arquivo 1-Tipos)

A maioria das entradas para o seu aplicativo vem em formato de texto simples. Esse texto poderia representar também um número ou uma data válida, mas você deve verificar isso para garantir que os dados sejam válidos.O .NET Framework possui alguns tipos internos que ajudam a converter dados de um tipo para outro.

Os métodos Parse e TryParse podem ser usados quando você possui uma string que deseja converter em um tipo de dados específico. Por exemplo, se você possui uma sequência que sabe ser um valor booleano, pode usar o método bool.Parse.

try
{
    string value = "true";
    bool b = bool.Parse(value);
    Console.WriteLine(b); //True
    Console.ReadKey();
}
catch (Exception)
{
    throw;
}

O método bool.Parse usa os campos estáticos somente leitura TrueString e FalseString para verificar se sua sequência é verdadeira ou falsa. Se sua string contiver um valor inválido, o Parse lançará uma FormatException. 

System.FormatException: 'Cadeia de caracteres não foi reconhecida como Boolean válido.'

Se você passar um valor nulo para a sequência, receberá um ArgumentNullException. 

System.ArgumentNullException: 'Valor não pode ser nulo. Arg_ParamName_Name'

A análise deve ser usada se você tiver certeza de que a análise será bem-sucedida. Se uma exceção for lançada, isso indica um erro real no seu aplicativo.

O TryParse faz as coisas de maneira diferente. Você usa o TryParse se não tiver certeza de que a análise será bem-sucedida. Você não deseja que uma exceção seja lançada e deseja lidar com a conversão inválida normalmente. Veja no exemplo de uso do método int.TryParse que analisa uma sequência de caracteres em um número válido.

string valuetry = "1";
int result;
bool success = int.TryParse(valuetry, out result);
if (success)
{
    Console.WriteLine("value is a valid integer");
}
else
{
    Console.WriteLine("value is not a valid integer");
}

Como mostra o exemplo, o TryParse retorna um valor booleano que indica se o valor pode ser analisado. O parâmetro out contém o valor resultante quando a operação é bem-sucedida. Se a análise for bem-sucedida, a variável reterá o valor convertido; caso contrário, ele contém o valor inicial.

O TryParse pode ser usado quando você estiver analisando alguma entrada do usuário. Se o usuário fornecer dados inválidos, você poderá mostrar uma mensagem de erro amigável e permitir que ele tente novamente. O código a seguir verifica se o costTextBox contém um valor de moeda válido:

valor = "-19,95";
decimal cost;
if (!decimal.TryParse(valor, NumberStyles.Currency,
        CultureInfo.CurrentCulture, out cost))
{
    // Display an error message.
    Console.WriteLine(d.ToString("Cost is not a valid currency value"));
}

Observe que alguns valores podem não fazer sentido agora, mas ainda devem ser permitidos porque, posteriormente, podem fazer sentido. Por exemplo, como mencionado anteriormente, o valor "-." não é um número de ponto flutuante válido, mas “–.1” é, portanto, o programa deve permitir “-.” enquanto o usuário estiver digitando.

Ao usar os métodos bool.Parse ou bool.TryParse, você não tem nenhuma opção de análise extra. Ao analisar números, você pode fornecer opções extras para o estilo do número e a cultura específica que deseja usar. O próximo exemplo mostra como você pode analisar uma sequência que contém um símbolo de moeda e um separador decimal. A classe CultureInfo pode ser encontrada no espaço para nome System.Globalization.

try
{
    CultureInfo english = new CultureInfo("En");
    CultureInfo holandês = new CultureInfo("Nl");
    string valor = "€19,95";
    decimal d = decimal.Parse(valor, NumberStyles.Currency, holandês);
    Console.WriteLine(d.ToString(english)); // Exibe 19,95
}
catch (Exception)
{
    throw;
}

Um problema mais complexo seria analisar uma data e hora. Você pode usar o método DateTime.Parse para isso, que oferece várias sobrecargas (métodos com o mesmo nome, mas com argumentos diferentes):

Chamada	Para	As informações de formatação são derivadas de
DateTime.ParseExact ou DateTime.TryParseExact
Analise uma cadeia de caracteres de data e hora que deve estar em um formato específico.	
sobrecarga de Parse(String)
Analise uma cadeia de caracteres de data e hora usando as convenções da cultura atual. Usa a cultura de encadeamento atual e o DateTimeStyles.AllowWhiteSpaces	A cultura do thread atual (propriedade DateTimeFormatInfo.CurrentInfo )


Chamada	Para		Provider	As informações de formatação são derivadas de
sobrecarga de Parse(String, IFormatProvider)
Analise uma cadeia de caracteres de data e hora usando as convenções de uma cultura específica. Usa a cultura especificada e os DateTimeStyles.AllowWhiteSpaces.	E PROVIDER É:	objeto DateTimeFormatInfo 
O objeto DateTimeFormatInfo especificado
			null	A cultura do thread atual (propriedade DateTimeFormatInfo.CurrentInfo )

sobrecarga de Parse(String, IFormatProvider, DateTimeStyles)
Analise uma cadeia de caracteres de data e hora com elementos de estilo especiais (como um espaço em branco ou nenhum espaço em branco).		objeto CultureInfo 
A propriedade CultureInfo.DateTimeFormat

sobrecarga de Parse(String, IFormatProvider, DateTimeStyles)
Analise uma cadeia de caracteres de data e hora e execute uma conversão para UTC ou hora local.		Implementação IFormatProvider personalizada	O método IFormatProvider.GetFormat


Ao analisar um DateTime, você deve levar em consideração coisas como diferenças de fuso horário e diferenças culturais, especialmente ao trabalhar em um aplicativo que usa a globalização. É importante analisar a entrada do usuário com a cultura correta.

O exemplo a seguir analisa a representação de cadeia de caracteres de vários valores de data e hora porParse(String), Parse(String, IFormatProvider) oe Parse(String, IFormatProvider, DateTimeStyles)

O algoritmo manipula a exceção FormatException que é lançada quando o método tenta analisar a representação da cadeia de caracteres de uma data e hora usando algumas convenções de formatação de outras culturas. Ele também mostra como analisar com êxito um valor de data e hora que não usa as convenções de formatação da cultura de thread atual.

static void Main(string[] args)
{
    // Define string representations of a date to be parsed.
    string[] dateStrings = {"01/10/2009 7:34 PM", "10.01.2009 19:34", "10-1-2009 19:34" };

    Parse_IFormatProvider(dateStrings);

    foreach (var data in dateStrings)
    {
        Parse_String(data);
        Parse_DateTimeStyles(data);
    }

    Console.ReadKey();
}

public static void Parse_String(string dateString)
{
    // Suponha que a cultura atual seja fr-FR.(igual pt-BR)
    // A data é 16 de fevereiro de 2008, 12 horas, 15 minutos e 12 segundos.
    // Use o valor padrão de data e hora nos fr-FR.(igual pt-BR)
    DateTime dateValue;

    try
    {
        dateValue = DateTime.Parse(dateString);
        Console.WriteLine("Data c/tempo fr-FR: '{0}' convertido em {1}.", dateString, dateValue);

        // Inverta mês e dia para se adaptar à cultura en-US.
        dateString = "2/16/2008 12:15:12";
        dateValue = DateTime.Parse(dateString);
        Console.WriteLine("Data c/tempo en-US: '{0}' convertido em {1}.", dateString, dateValue);

        // Chame outra sobrecarga do Parse para converter a string com êxito
        // formatado de acordo com as convenções da cultura en-US
        dateValue = DateTime.Parse(dateString, new CultureInfo("en-US", false));
        Console.WriteLine("Data c/tempo en-US: '{0}' convertido em {1}.", dateString, dateValue);

        // Analisar string com data, mas sem componente de hora.
        dateString = "16/02/2008";
        dateValue = DateTime.Parse(dateString);
        Console.WriteLine("Data s/tempo: '{0}' convertido em {1}.", dateString, dateValue);
    }
    catch (FormatException)
    {
        Console.WriteLine("Não foi possível converter '{0}'.", dateString);
    }
}

public static void Parse_IFormatProvider(string[] dateStrings)
{
    // Define cultures to be used to parse dates.
    CultureInfo[] cultures = {CultureInfo.CreateSpecificCulture("en-US"),
                    CultureInfo.CreateSpecificCulture("fr-FR"),
                    CultureInfo.CreateSpecificCulture("de-DE"),
                    CultureInfo.CreateSpecificCulture("pt-BR")};

    // Parse dates using each culture.
    foreach (CultureInfo culture in cultures)
    {
        DateTime dateValue;
        Console.WriteLine("Attempted conversions using {0} culture.", culture.Name);
        foreach (string dateString in dateStrings)
        {
            try
            {
                dateValue = DateTime.Parse(dateString, culture);
                Console.WriteLine("Converted '{0}' to {1}.", 
                            dateString, dateValue.ToString("f", culture));
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert '{0}' for culture {1}.", 
                            dateString, culture.Name);
            }
        }
        Console.WriteLine();
    }
}

public static void Parse_DateTimeStyles(string dateString)
{
    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
    DateTimeStyles styles = DateTimeStyles.None;
    DateTime result;

    try
    {
        result = DateTime.Parse(dateString, culture, styles);
        Console.WriteLine("{0} converted to {1} {2}.", 
                    dateString, result, result.Kind.ToString());

        // Parse the same date and time with the AssumeLocal style.
        styles = DateTimeStyles.AssumeLocal;
        result = DateTime.Parse(dateString, culture, styles);
        Console.WriteLine("{0} converted to {1} {2}.", 
                    dateString, result, result.Kind.ToString());

        // Parse a date and time that is assumed to be local.
        // This time is five hours behind UTC. The local system's time zone is 
        // eight hours behind UTC.
        dateString = "2009/03/01T10:00:00-5:00";
        styles = DateTimeStyles.AssumeLocal;
        result = DateTime.Parse(dateString, culture, styles);
        Console.WriteLine("{0} converted to {1} {2}.", 
                    dateString, result, result.Kind.ToString());

        // Attempt to convert a string in improper ISO 8601 format.
        dateString = "03/01/2009T10:00:00-5:00";
        result = DateTime.Parse(dateString, culture, styles);
        Console.WriteLine("{0} converted to {1} {2}.", 
                    dateString, result, result.Kind.ToString());

        // Assume a date and time string formatted for the fr-FR culture is the local 
        // time and convert it to UTC.
        dateString = "2008-03-01 10:00";
        culture = CultureInfo.CreateSpecificCulture("fr-FR");
        styles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal;

        result = DateTime.Parse(dateString, culture, styles);
        Console.WriteLine("{0} converted to {1} {2}.", 
                    dateString, result, result.Kind.ToString());
    }
    catch (FormatException)
    {
        Console.WriteLine("Unable to convert {0} to a date and time.", dateString);
    }
}

Classe Convert (veja mais detalhes no arquivo 1-Tipos)

O .NET Framework também oferece a classe Convert para converter entre tipos de base. Os tipos de base suportados são Boolean, Char, SByte, Byte, Int16, Int32, Int64, UInt16, Uint32, Uint64, Único, Duplo, Decimal, DateTime e String. A diferença entre Parse/TryParse e Convert é que o Convert habilita valores nulos. Ele não lança uma ArgumentNullException; em vez disso, ele retorna o valor padrão para o tipo fornecido, como mostra abaixo.

int i = Convert.ToInt32(null);
Console.WriteLine(i); // 0
Console.ReadKey();

Outra diferença entre os métodos Convert e Parse é que o Parse usa uma string apenas como entrada, enquanto o Convert também pode usar outros tipos de base como entrada. Abaixo, um exemplo de conversão de um duplo em um int. O valor duplo é arredondado.

double d = 23.15;
int inteiro = Convert.ToInt32(d);
Console.WriteLine(inteiro); //23

Métodos como esses lançam uma OverflowException quando o valor analisado ou convertido é muito grande para o tipo de destino.No exame, é importante saber que, ao analisar a entrada do usuário, a melhor opção é o método TryParse. Lançar exceções para erros "normais" não é uma prática recomendada. O TryParse apenas retorna falso quando o valor não pode ser analisado.

Usando métodos String

Um programa pode usar a propriedade Length do tipo de dados da string para determinar se a string está em branco. Isso permite que você decida facilmente se o usuário deixou um campo obrigatório em branco em um formulário. A classe string também fornece vários métodos que são úteis para executar validações de string mais complicadas. A tabela abaixo resume o mais útil desses métodos.
Método	Descrição
Contains	Retorna true se a string contiver uma substring especificada. Por exemplo, você pode usar isso para determinar se um endereço de email contém o caractere @.
EndsWith	Retorna true se a string termina com uma substring especificada.
IndexOf	Retorna o local de uma substring especificada na string, iniciando opcionalmente a pesquisa em uma posição específica.
IndexOfAny	Retorna a localização de qualquer um de um conjunto especificado de caracteres dentro da sequência, opcionalmente iniciando em uma posição específica.
IsNullOrEmpty	Retornará true se a sequência for nula ou em branco.
IsNullOrWhitespace	Retorna true se a sequência for nula, em branco ou contiver apenas caracteres de espaço em branco, como espaços e tabulações.
LastIndexOf	Retorna o último local de uma substring especificada na string, iniciando opcionalmente a pesquisa em uma posição específica.
LastIndexOfAny	Retorna o último local de qualquer um de um conjunto especificado de caracteres dentro da sequência, iniciando opcionalmente em uma posição específica.
Remove	Remove caracteres da string. Por exemplo, você pode remover os caracteres - do número de telefone 234-567-8901 e, em seguida, examinar o resultado para ver se faz sentido.
Replace	Substitui as instâncias de um caractere ou substring por um novo valor.
Split	Retorna uma matriz que contém substrings delimitados por um determinado conjunto de caracteres. Por exemplo, você pode dividir o número de telefone 234-567-8901 em partes e examiná-los separadamente.
StartsWith	Retorna true se a sequência começar com uma substring especificada.
Substring	Retorna uma substring em um local especificado.
ToLower	Retorna a string convertida em minúscula. Isso pode ser útil se você quiser ignorar o caso da string.
ToUpper	Retorna a string convertida em maiúscula. Isso pode ser útil se você quiser ignorar o caso da string.
Trim	Retorna a sequência com os caracteres de espaço em branco à esquerda e à direita removidos.
TrimEnd	Retorna a string com os caracteres de espaço em branco à direita removidos.
TrimStart	Retorna a string com os principais caracteres de espaço em branco removidos.

Com bastante trabalho, você pode usar esses métodos de string para executar todos os tipos de validações. Por exemplo, suponha que o usuário insira um número de telefone no formato (234) 567-8901. Você pode usar o método Split para dividir isso nas partes 234, 567 e 8901. Você pode verificar se a divisão retornou três partes e se as peças têm os comprimentos necessários.

Embora você possa usar os métodos de cadeia para executar praticamente qualquer validação, às vezes isso pode ser difícil porque as validações podem ser complexas. Por exemplo, (234)567-8901 não é o único formato de número de telefone possível nos EUA. Você também pode desejar que o programa permita 234-567-8901, 1 (234) 567-8901, 1-234-567-8901, + 1-234-567-8901, 234.567.8901 e outros formatos.

Os números de telefone de outros países, endereços de email, códigos postais, números de série e outros valores também podem exigir validações complicadas. Você pode executar tudo isso usando os métodos da classe string, mas às vezes pode ser difícil. Nesses casos, muitas vezes você pode usar as expressões regulares descritas na seção a seguir para validar a estrutura mais complexa que essas entidades possuem.

Usando Expressões Regulares

Uma expressão regular é um padrão específico usado para analisar e encontrar correspondências em strings. Uma expressão regular às vezes é chamada regex ou regexp. Expressões regulares são flexíveis. Por exemplo, o regex ^(\(\d{3}\)|^\d{3}[.-]?)?\d{3}[.-]?\d{4}$ corresponde ao telefone norte-americano comcódigo de área (com ou sem parênteses) e os números (com ou sem hífens ou pontos). A classe Regex contém métodos e propriedades para validar um texto com um padrão de caracteres específico; alguns deles estão listados abaixo.
Método	Descrição
IsMatch(string input)	Retorna true se a expressão regular especificada no construtor Regex corresponder à seqüência de entrada especificada.
IsMatch(string input, int startat)	Retorna true se a expressão regular especificada no construtor Regex corresponde à sequência de entrada especificada e começa na posição inicial especificada da sequência.
IsMatch(string input, string pattern)	Retornará true se a expressão regular especificada corresponder à sequência de entrada especificada.
Matches(string input)	Procura na cadeia de entrada especificada todas as ocorrências de uma expressão regular.
Match(string InputStr, string Pattern)	Corresponde à string de entrada com um padrão de string.
Replace(string input, string replacement)	Em uma sequência de entrada especificada, substitui todas as sequências que correspondem a um padrão de expressão regular por uma sequência de substituição especificada.
Split	Divide uma sequência em uma matriz de substrings delimitadas por partes da sequência que correspondem a uma expressão regular.

Muitos dos métodos descritos acima têm várias versões sobrecarregadas. Em particular, muitos usam uma string como parâmetro e, opcionalmente, podem usar outro parâmetro que fornece uma expressão regular. Se você não passar uma expressão regular para o método, ele usará a expressão que você passou o construtor do objeto.

A classe Regex também fornece versões estáticas desses métodos que usam uma string e uma expressão regular como parâmetros. Por exemplo, o código a seguir valida o texto em um TextBox e define sua cor de plano de fundo para fornecer ao usuário uma dica sobre se o valor corresponde a uma expressão regular.	

private static void ValidateTextBox(string txt, bool allowBlank, string pattern)
{
    // Assume it's invalid.
    bool valid = false;
    var BackColor = new Color();

    // If the text is blank, allow it.
    string text = txt;
    if (allowBlank && (text.Length == 0)) valid = true;
    // If the regular expression matches the text, allow it.
    if (Regex.IsMatch(text, pattern)) valid = true;
    // Display the appropriate background color.
    if (valid) BackColor = SystemColors.Window;
    else BackColor = Color.Yellow;
}

A seguir um exemplo de uso Regex em conjunto com o método IsMatch e IsMatched:

//Pattern for Matching Pakistan's Phone Number
string pattern = @"\(\+92\)\s\d{3}-\d{3}-\d{4}";

string inputStr = "(+92) 336-071-7272";
bool isMatched = Regex.IsMatch(inputStr, pattern);
if (isMatched == true)
    Console.WriteLine("Pattern for phone number is matched with inputStr");
else
    Console.WriteLine("Pattern for phone number is not matched with inputStr");

Existem algumas linguagens de expressão regular diferentes usadas por diferentes linguagens de programação e ambientes. Como esses idiomas são semelhantes, mas não idênticos, é fácil confundir e usar a sintaxe errada. Se você achar que uma expressão não faz o que pensa, verifique se está usando a sintaxe correta para C#. Em particular, se você usar a Internet para encontrar uma expressão que corresponda a algum formato padrão, como números de telefone do Reino Unido ou códigos postais do Canadá, verifique se o site em que você encontrou as expressões usa a sintaxe exigida pelo C#.

Uma expressão regular é uma combinação de caracteres literais e caracteres que têm significados especiais. Por exemplo, a sequência [a-z] significa que o objeto Regex deve corresponder a qualquer caractere único no intervalo "a" a "z". Expressões regulares também podem incluir seqüências de caracteres especiais chamadas seqüências de escape que representam algum valor especial. Por exemplo, a sequência \b corresponde a um limite de palavra e \d corresponde a qualquer dígito de 0 a 9.

Opções do Regex

Um programa pode especificar opções de expressão regular de três maneiras.
1.	pela enumeração RegexOptions: pode passar um parâmetro de opções para os métodos de correspondência de padrão de um objeto Regex, como IsMatch. 

string pattern0 = @"d \w+ \s";
string input0 = "Dogs are decidedly good pets.";
RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
Regex.Matches(input0, pattern0, options)

2.	pode usar a sintaxe (opções?): Aqui, as opções podem incluir qualquer um dos valores i, m, n, s ou x. (Eles serão descritos em breve.) Se a lista começar com um caractere -, as seguintes opções serão desativadas. as opções permanecem em vigor até que um novo conjunto de opções em linha redefina seus valores.

string pattern1 = @"(?ix) d \w+ \s";
string input1 = "Dogs are decidedly good pets.";
Regex.Matches(input1, pattern1)

3.	pode usar a sintaxe (? Opções: subexpressão) em uma expressão regular. Nesse caso, as opções são como antes e a subexpressão faz parte de uma expressão regular durante a qual as opções devem ser aplicadas.

string pattern2 = @"\b(?ix: d \w+)\s";
string input2 = "Dogs are decidedly good pets.";
Regex.Matches(input2, pattern2)

Todos os exemplos acima habilita correspondência sem diferenciação entre maiúsculas e minúsculas e ignorar espaço em branco do parâmetro ao identificar palavras que começam com a letra "d". A tabela abaixo descreve as opções disponíveis.
Padrão	Significado
i	Ignore maiúsculas e minúsculas.
m	Multilinha. Aqui ^ e $ correspondem ao início e ao final das linhas.
s	Linha única. Aqui, . corresponde a todos os caracteres, incluindo \n. (Veja a entrada para . na tabela de Classes de caracteres abaixo)
n	Captura explícita. Não capture grupos sem nome. Consulte a seção “Agrupando construções” abaixo  para obter mais informações sobre grupos.
x	Ignore o espaço em branco sem escape no padrão e ative os comentários após o caractere #.

PARTES MAIS ÚTEIS DE REGEX

Às vezes, um programa precisa usar um caractere como ele mesmo, embora pareça um caractere especial. Por exemplo, o caractere [ normalmente inicia um intervalo de caracteres. Se você quiser usar [ em si, você "escapará" adicionando um \ na frente dele como em \[. Por exemplo, a sequência um pouco confusa [\[-\]] corresponde ao intervalo de caracteres entre [e].

As partes mais úteis de uma expressão regular podem ser divididas em:
 
•	Escapes de Caracteres
•	Classes de Caracteres
•	Âncoras
•	Construções de Agrupamento
•	Quantificadores
•	Construções de Alternância 

As seções a seguir descrevem cada uma delas. Consulte os links na seção “Leitura e recursos adicionais”, posteriormente neste capítulo, para obter informações sobre outros recursos de expressões regulares.

Escapes de Caracteres

A tabela abaixo lista as fugas de caracteres de expressão regular mais úteis.
Padrão	Significado
\t	Corresponde tab
\r	Corresponde return
\n	Corresponde à nova linha
\nnn	Corresponde a um caractere com o código ASCII fornecido pelos dois ou três dígitos octal nnn
\xnn	Corresponde a um caractere com o código ASCII fornecido pelos dois dígitos hexadecimais nn
\unnnn	Corresponde a um caractere com a representação Unicode fornecida pelos quatro dígitos hexadecimais nnnn

Classes de caracteres

Uma classe de caracteres corresponde a qualquer um de um conjunto de caracteres. A tabela abaixo descreve as construções de classe de caracteres mais úteis.
Padrão	Significado
[chars]	Corresponde a um único caractere entre colchetes. Por exemplo, [aeiou] corresponde a uma vogal única em minúscula.
[^chars	Corresponde a um único caractere que não está entre colchetes. Por exemplo, [^ aeiouAEIOU] corresponde a um único caractere que não é de vogal, como x, 7 ou &.
[first-last]	Corresponde a um único caractere entre o primeiro e o último. Por exemplo, [a-zA-Z] corresponde a qualquer letra minúscula ou maiúscula.
.	Um curinga que corresponde a qualquer caractere único, exceto \ n. (Para corresponder a um período, use a sequência de escape \.)
\w	Corresponde a um caractere de uma palavra Normalmente, isso é equivalente a [a-zA-Z_0-9]
\W	Corresponde a um único caractere não palavra Normalmente isso é equivalente a [^ a-zA-Z_0-9]
\s	Corresponde a um único caractere de espaço em branco Normalmente, isso inclui espaço, avanço de formulário, nova linha, retorno, guia e guia vertical
\S	Corresponde a um único caractere sem espaço em branco Normalmente, isso corresponde a tudo, exceto espaço, feed de formulário, nova linha, retorno, guia e guia vertical
\d	Corresponde a um único dígito decimal Normalmente isso é equivalente a [0-9]
\D	Corresponde a um único caractere que não é um dígito decimal. Normalmente isso é equivalente a [^ 0-9]

Anchors 

Uma anchor, ou asserção atômica de largura zero, representa um estado em que a string deve estar em um determinado ponto. Âncoras não consomem caracteres. Por exemplo, os caracteres ^ e $ representam o início e o final de uma linha ou sequência, dependendo se o objeto Regex está funcionando no modo de linha única ou multilinha.
Padrão	Significado
^	Corresponde ao início da linha ou sequência
$	Corresponde ao final da string ou antes do \ n no final da linha ou string
\A	Corresponde ao início da string
\z	Corresponde ao final da string
\Z	Corresponde ao final da sequência ou antes de \ n no final da sequência
\G	Partidas onde a partida anterior terminou
\B	Corresponde a um limite que não é uma palavra

Agrupando Construções

As construções de agrupamento permitem definir grupos de partes correspondentes de uma sequência. Por exemplo, em um número de telefone nos EUA com o formato 234-567-8901, você pode definir grupos para conter as peças 234, 567 e 8901. O programa pode posteriormente se referir a esses grupos com código ou posteriormente na mesma expressão regular .

Por exemplo, considere a expressão (\w)\1. Os parênteses criam um grupo numerado que, neste exemplo, corresponde a um caractere de palavra única. O \1 refere-se ao grupo numerado 1. Isso significa que essa expressão regular corresponde a um caractere de palavra seguido por ele mesmo. Se a sequência for "livro", esse padrão corresponderá ao "oo" no meio.

Existem vários tipos de grupos, alguns dos quais são bastante especializados e confusos. Os dois mais comuns são grupos numerados e nomeados. Para criar um grupo numerado, basta colocar uma subexpressão regular entre parênteses, conforme mostrado no exemplo anterior (\w)\1. Observe que a numeração do grupo começa com 1, não com 0.

Para criar um grupo nomeado, use a sintaxe (? <name> subexpressão) em que name é o nome que você deseja atribuir ao grupo e a subexpressão é uma subexpressão regular. Por exemplo, a expressão (?<twice>\w)\k<twice> é semelhante à versão anterior, exceto que o grupo é nomeado duas vezes.

Aqui, o \k faz a expressão corresponder à substring correspondida pelo grupo nomeado a seguir, neste caso duas vezes.

Quantificadores

Os quantificadores fazem o mecanismo de expressão regular corresponder ao elemento anterior um certo número de vezes. Por exemplo, a expressão \d{3} corresponde a qualquer dígito exatamente três vezes. A tabela abaixo descreve quantificadores de expressão regular.
Padrão	Significado
*	Corresponde ao elemento anterior 0 ou mais vezes
+	Corresponde ao elemento anterior 1 ou mais vezes
?	Corresponde ao elemento anterior 0 ou 1 vezes
{n}	Corresponde ao elemento anterior exatamente n vezes
{n,}	Corresponde ao elemento anterior n ou mais vezes
{n, m}	Corresponde ao elemento anterior entre n e m vezes (inclusive)

Se você seguir um destes com ?, O padrão corresponderá o menor número de vezes possível. Por exemplo, o padrão bo + corresponde a b seguido por 1 ou mais ocorrências da letra o, portanto, corresponde ao "boo" no "livro". O padrão bo +? também corresponde a b, seguido por 1 ou mais ocorrências da letra o, mas corresponde ao menor número possível de letras, portanto corresponderia ao "bo" no "livro".

Construções de alternância

Construções de alternância usam o | para permitir que um padrão corresponda a uma das duas subexpressões. Por exemplo, a expressão (yes | no) corresponde sim ou não.

Expressões regulares úteis

O código a seguir mostra uma maneira que garante que a sequência de entrada esteja de acordo com o padrão do número de telefone do Paquistão. Aqui está como funciona:
@“\(\+92\)\s\d{3}-\d{3}-\d{4}”;

Padrão	Significado
\(	corresponde a (
\+	corresponde a +
92	corresponde a 92
\)	corresponde a )
\s	corresponde a um espaço 
\d{3}	Corresponde a 3 dígitos numéricos, o equivalente a 456
-	corresponde a -
\d{3}	Corresponde a 3 dígitos numéricos, o equivalente a 456
-	corresponde a -
\d{4}	Corresponde a 4 dígitos numéricos, o equivalente a 4561

A sequência de padrões contém um conjunto de caracteres, que garante que a sequência de entrada esteja de acordo com o padrão de ID do email. Aqui está como funciona:
@“^\w+[a-zA-Z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+)\.\w{2,4}”

Padrão	Significado
^	Corresponde a tudo, desde o início
\w+	Informa que deve haver pelo menos um ou mais alfabetos
[a-zA-Z0-9]+	Informa que deve haver um ou mais caracteres alfanuméricos
[-._]	Informa que pode haver qualquer caractere especial incluído, ou seja, -._
([-._][a-z0-9]+)*	indica que pode haver um caractere especial e valores alfanuméricos
@	corresponde a @
\.	corresponde a um ponto
\w{2,4}	Diz que deve haver no mínimo 2 ou no máximo 4 palavras

Expressões regulares têm uma reputação de serem difíceis de escrever e usar, porém, muitos padrões já foram escritos por outra pessoa. Sites como http://regexlib.com/ contêm inúmeros exemplos que você pode usar ou adaptar às suas próprias necessidades. Expressões regulares podem ser úteis ao validar a entrada do aplicativo, reduzindo para algumas linhas de código o que pode levar dezenas ou mais com codificação manual. Pode ter situações onde deva se validar a inserção de barras e hífens. Ou permissão de incluir espaço em branco ao inserir um CEP. O código abaixo mostra como é complicado validar um CEP holandês manualmente.

static void Main(string[] args)
{
    string[] zipCodes = { "1234AB", "1234 AB", "1001 AB", "03345-001" };

    foreach (var zipCode in zipCodes)
    {
        Console.WriteLine(ValidateZipCode(zipCode));
        Console.WriteLine(ValidateZipCodeRegEx(zipCode));
    }

    RegexOptions options = RegexOptions.None; Regex regex = new Regex(@"[ ]{2,}", options);
    string input = "1       2          3  4       5";
    string result = regex.Replace(input, " ");
    Console.WriteLine(result); // 1 2 3 4 5 

    Console.ReadKey();
}

static bool ValidateZipCode(string zipCode)
{
    // Valid zipcodes: 1234AB | 1234 AB | 1001 AB 
    if (zipCode.Length < 6) return false;
    string numberPart = zipCode.Substring(0, 4);
    int number;

    if (!int.TryParse(numberPart, out number))
        return false;

    string characterPart = zipCode.Substring(4);

    if (numberPart.StartsWith("0")) return false;
    if (characterPart.Trim().Length < 2) return false;
    if (characterPart.Length == 3 && characterPart.Trim().Length != 2)
        return false;

    return true;
}

Se você usar uma expressão regular, o código será muito menor. Uma expressão regular que corresponde aos códigos postais holandeses é ^[1-9][0-9]{3}\s?[a-zA-Z]{2}$.

Você pode usar esse padrão com a classe RegEx que pode ser encontrada no namespace System.Text.RegularExpressions. A função abaixo mostra como você pode usar a classe RegEx para validar um código postal.

static bool ValidateZipCodeRegEx(string zipCode)
{
    Match match = Regex.Match(zipCode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$", RegexOptions.IgnoreCase);
    return match.Success;
}

Além de corresponder a entrada do aplicativo a um padrão específico, você também pode usar expressões regulares para garantir que a entrada não contenha determinados caracteres restritos. Você pode usar regex para substituir esses caracteres por outro valor para removê-los da entrada.

Especialmente ao trabalhar no contexto de um aplicativo Web, é importante filtrar a entrada do usuário. Imagine que um usuário insira algum HTML dentro de um campo de entrada destinado a informações como nome ou endereço. O aplicativo não valida a entrada e a salva diretamente no banco de dados. Na próxima vez que o usuário visitar o aplicativo, o HTML será renderizado diretamente como parte da página. Um usuário pode causar muitos danos ao usar essa técnica, por isso é importante garantir que a entrada não contenha caracteres potencialmente prejudiciais.

O código abaixo mostra um exemplo de uso de uma expressão RegEx para remover todo o uso excessivo de espaço em branco. Cada espaço é permitido, mas vários são substituídos por um único espaço.

RegexOptions options = RegexOptions.None; Regex regex = new Regex(@"[ ]{2,}", options);
string input = "1       2          3  4       5";
string result = regex.Replace(input, " ");
Console.WriteLine(result); // 1 2 3 4 5

Embora o regex pareça mais difícil do que escrever o código de validação em C # simples, definitivamente vale a pena aprender como ele funciona. Uma expressão regular pode simplificar drasticamente o seu código e vale a pena examinar se você está em uma situação que exige validação.

VALIDATING JSON AND XML

Ao trocar dados com outros aplicativos, geralmente você recebe dados JavaScript Object Note JavaScripttion (JSON) ou XML (Extensible Markup Language). JSON é um formato popular que tem suas raízes no JavaScript. É uma maneira compacta de representar alguns dados. O XML tem um esquema mais rígido e é considerado mais detalhado, mas certamente tem seus usos. É importante garantir que esses dados sejam válidos antes de você começar a usá-los.

Formato JSON

O formato JSON começa com chaves {} ou colchetes []. Você pode ver facilmente se uma sequência começa com esses caracteres, mas, verificar apenas os caracteres inicial e final não é suficiente para saber se todo o objeto pode ser analisado como JSON. O .NET Framework oferece o JavaScriptSerializer que você pode usar para desserializar uma string JSON em um objeto. Você pode encontrar o JavaScriptSerializer nonamespace System.Web.Extensions.dll.

O exemplo abaixo mostra como você pode usar o JavaScriptSerializer. Nesse caso, você está desserializando os dados para um dicionário <string, objeto>. Você pode procurar ou pesquisar nomes de propriedades e seus valores.

private class Teacher
{
    private int id { get; set; }
    public string name { get; set; }
    public long salary { get; set; }
}

static void Main(string[] args)
{
    // Criou a instância e inicializou
    Teacher professor = new Teacher()
    {
        name = "Raimundo Nonato",
        salary = 1000,
    };

    JavaScriptSerializer dataContract = new JavaScriptSerializer();
    string serializedDataInStringFormat = dataContract.Serialize(professor);
    Console.WriteLine("A serialização JavaScript foi concluída!");
    //serializedDataInStringFormat = "ddd";
    IsJson(serializedDataInStringFormat);
    Console.ReadKey();
}

public static bool IsJson(string json)
{
    try
    {
        json = json.Trim();
        var result = json.StartsWith("{") && json.EndsWith("}") || 
                        json.StartsWith("[") && json.EndsWith("]");

        Console.WriteLine(result);

        var serializer = new JavaScriptSerializer();
        var deserialize = serializer.Deserialize<Dictionary<string, object>>(json);

        Console.WriteLine(deserialize);

        return result;
    }
    catch (Exception)
    {
        throw;
    }
}

Se você passar algum JSON inválido para oJavaScriptSerializer, uma ArgumentException será lançada uma mensagem.

System.ArgumentException: 'JSON primitivo inválido: ddd.'


XSD (XML Schema Definition) 

XML Schema é uma linguagem baseada no formato XML para definição de regras de validação ("esquemas") em documentos no formato XML. Foi a primeira linguagem de esquema para XML a obter o status de recomendação por parte do W3C. Esta linguagem é uma alternativa ao DTD (Document Type Definition), cuja sintaxe não é baseada no formato XML.

Em meados de 1999, o consórcio W3C (responsável por vários padrões da WEB) publicou pela primeira vez o XML Schema. Sendo a primeira iniciativa de apresentar uma alternativa ao padrão DTD. Após diversas revisões, em 2001, foi lançada a recomendação final do XML Schema e em 2004 houve algumas adaptações. Dessa data até hoje, diversos fabricantes têm incluído esse padrão em seus produtos.

A utilização de DTDs teve grande importância quando o padrão XML surgiu. Pois era o padrão para validação de documentos da linguagem antecessora da XML (o SGML) e foi utilizada como forma de validação também para o XML. Embora seja capaz de realizar a validação de arquivos XML, a DTD possui muitas limitações. Não existe um conjunto amplo de tipos (todos os dados são interpretados como texto), trazendo efeitos desagradáveis como validação, comparação, etc. Também não suportam namespaces, o que força que os elementos sempre apareçam na ordem especificada, etc.

Um arquivo contendo as definições na linguagem XML Schema é chamado de XSD (XML Schema Definition) e são usados para descrever o “formato/padrão” que um arquivo XML deve seguir, ou seja, ele tem que indicar quais nodes, subnodes (<node1><subnode1/></node1>), quais atributos eles podem conter e muito mais. O XSD indica também o tipo dos valores que esses nodes e atributos (<node1 atributo1=’abc’/>) podem armazenar, o tamanho dos dados caso se aplique (string de 10 caracteres), se um determinado node é obrigatório ou não (nullable=”true”), quais possíveis valores uma enumeração pode assumir, etc…

O padrão XSD consegue suprir as limitações da DTD, além de fornecer diversas funcionalidades, é possível construir tipos próprios derivados dos tipos básicos, realizar relacionamentos entre elementos de dados dentro do XML (similar aos relacionamentos entre tabelas), etc.

O XSD, que é escrito em XML, significa que não requer processamento intermediário por um analisador ao contrário das linguagens de esquema XML anteriores, como DTD ou SOX (Simple Object XML). Outros benefícios incluem auto-documentação, criação automática de esquema e a capacidade de ser consultada por meio de XML Transformations (XSLT).

Foi amplamente utilizado para desenvolvimento da NF-e (Nota Fiscal Eletrônica) Brasileira. Porém, existem muitos desafios e limitações com o XSD também. Alguns detratores argumentaram que é desnecessariamente complexo, carece de uma descrição matemática formal e tem suporte limitado para conteúdo não ordenado.

Se você possui um arquivo XSD e deseja gerar um arquivo XML de amostra para visualizar dados xml, aqui está uma maneira rápida de gerar dados XML usando o Visual Studio.

1.	Arraste o arquivo XSD no Visual Studio ou vá para Visual Studio> Arquivo> Abrir> Arquivo e selecione o arquivo XSD a ser aberto.
2.	Clique em "XML Schema Explorer"
3.	 
4.	No "XML Schema Explorer", role até o fim para encontrar o nó raiz/dados. Clique com o botão direito do mouse no nó raiz/dados e ele mostrará "Gerar XML de Amostra". Se não aparecer, significa que você não está no nó do elemento de dados, mas em qualquer um dos nós de definição de dados.
 

Caso não tenha o XSD, você pode gerar arquivos XSD a partir de classes do próprio C# para que sejam usadas em métodos de serialização/deserialização, utilizando, entre outros, o aplicativo XSD.exe da Microsoft.

A primeira etapa é gerar um esquema XML a partir do arquivo. Pegue, por exemplo, o arquivo XML abaixo:

<?xml version="1.0" encoding="utf-8" ?>
<Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <FirstName>John</FirstName>
  <LastName>Doe</LastName>
  <Age>42</Age>
</Person>

Você pode criar um arquivo XSD para esse esquema usando a XML Schema Definition Tool (Xsd.exe) que faz parte do Visual Studio. Essa ferramenta pode gerar arquivos de esquema XML ou classes C#.

Você precisará executar a ferramenta XSD.EXE no prompt de comando do Visual Studio (Iniciar -> Visual Studio  -> Ferramentas do Visual Studio -> prompt de comando do Visual Studio), pois não há front-end gráfico para a ferramenta.

Navegue até a pasta do projeto onde está lozalizado o arquivo person.xml e digite a seguinte linha de comando para gerar um arquivo XSD:
Xsd.exe person.xml
 

A ferramenta cria um arquivo chamado person.xsd. Você pode ver o conteúdo desse arquivo XSD na pasta do projeto.
 
 

<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="Person">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="FirstName" type="xs:string" minOccurs="0" />
        <xs:element name="LastName" type="xs:string" minOccurs="0" />
        <xs:element name="Age" type="xs:string" minOccurs="0" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element ref="Person" />
      </xs:choice>
    </xs:complexType>
  </xs:element>
</xs:schema>

Por padrão, nenhum dos itens no arquivo é necessário. No entanto, ele registra quais elementos são possíveis e como deve ser a estrutura do arquivo.Agora você pode usar esse arquivo XSD para validar um arquivo XML. O código abaixo mostra uma maneira de fazer isso.

public static void ValidateXML()
{
    string xsdPath = "person.xsd";
    string xmlPath = "person.xml";
    XmlReader reader = XmlReader.Create(xmlPath);
    XmlDocument document = new XmlDocument();
    document.Schemas.Add("", xsdPath);
    document.Load(reader);
    ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
    document.Validate(eventHandler);
}

static void ValidationEventHandler(object sender, ValidationEventArgs e)
{
    switch (e.Severity)
    {
        case XmlSeverityType.Error:
            Console.WriteLine("Error: {0}", e.Message);
            break;
        case XmlSeverityType.Warning:
            Console.WriteLine("Warning {0}", e.Message);
            break;
    }
}

Se houver algo errado com o arquivo XML, como um elemento não existente, o ValidationEventHandler será chamado. Dependendo do tipo de erro de validação, você pode decidir qual ação executar.

Outro exemplo mais detalhado do XSD..

<?xml version="1.0" encoding="utf-8" ?>
<beginnersbook>
  <to>My Readers</to>
  <from>Chaitanya</from>
  <subject>A Message to my readers</subject>
  <message>Welcome to beginnersbook.com</message>
</beginnersbook>

<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="beginnersbook">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="to" type="xs:string" minOccurs="0" />
        <xs:element name="from" type="xs:string" minOccurs="0" />
        <xs:element name="subject" type="xs:string" minOccurs="0" />
        <xs:element name="message" type="xs:string" minOccurs="0" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element ref="beginnersbook" />
      </xs:choice>
    </xs:complexType>
  </xs:element>
</xs:schema>

No exemplo acima, vimos que a tag elemento define que “beginnersbook” como nome de um elemento e que ele é do tipo complexType. Logo abaixo a tag sequence define que o elemento complexo “beginnersbook é uma sequência de elementos mais simples com o tipo definidos como string (type=”xs:string”)
No esquema XML, um elemento pertence a um dos dois tipos a seguir.
1.	SimpleType: Um elemento singleType pode conter texto, eles não contêm outros elementos. 
2.	ComplexType: Um elemento complexType pode conter atributos, outros elementos e texto. 

Os elementos são declarados utilizando-se a tag “element”. Os principais atributos da tag elemento são:
Atributo	Especificação
name	especifica o nome do elemento
type	especifica o tipo de dados do elemento
minOccurs	especifica o mínimo de vezes que o elemento pode aparecer
maxOccurs	especifica o máximo de vezes que o elemento pode aparecer (a palavra unbounded pode ser utilizada para especificar uma quantidade ilimitada).

A declaração abaixo cria um elemento chamado “beginnersbook” tipo string, que pode aparecer no mínimo zero (0) vezes e no máximo uma (1) vez:

<xsd:element name="beginnersbook" type="xsd:string" minOccurs="0" maxOccurs="1"/>

De uma forma geral as declarações de atributos se parecem muito com as declarações de elementos. Essas declarações possuem alguns atributos. Os principais são:
Atributo	Especificação
name	especifica o nome do atributo
type	especifica o tipo de dados do atributo
use	especifica a utilização do atributo (requerido, opcional ou proibido)

Ex.: declara dois atributos. O primeiro é do tipo data e é opcional. O segundo é do tipo string e é obrigatório.

<xsd:attribute name="datacadastro" type="xsd:dateTime" use="optional"/>
<xsd:attribute name="estadocivil" type="xsd:string" use="required"/>

Realizar criptografia simétrica e assimétrica 
•	Escolher um algoritmo de criptografia apropriado; gerenciar e criar certificados; implementar gerenciamento de chave; implementar o namespace System.Security; fazendo hash de dados; criptografar fluxos

CRIPTOGRAFANDO E DESCRIPTOGRAFANDO

A segurança é uma parte importante a ser considerada ao desenvolver um aplicativo, para que a privacidade dos dados, autenticidade do usuário, segurança da transferência de dados não sejam comprometidos.Com a criptografia, você pega um pedaço de texto sem formatação (texto normal legível por humanos) e, em seguida, executa um algoritmo sobre ele. Os dados resultantes se parecem com uma sequência de bytes aleatória, geralmente chamada de texto cifrado. A descriptografia é o processo oposto: a sequência de bytes é transformada nos dados de texto sem formatação originais.

1. Criptografia é o processo de conversão de texto sem formatação em texto cifrado.
2. Descriptografia é o processo de conversão de texto cifrado em texto sem formatação.

Algoritmos públicos

Como o algoritmo é público, a chave é o que deve ser mantido em sigilo. Você pode usar algoritmos altamente complexos para criptografar seus dados, mas se não conseguir manter suas senhas e códigos em segredo, todos poderão ler seus dados particulares. Na criptografia, você pode:
1.	Manter seu algoritmo em segredo 
2.	Usar um algoritmo público e manter sua chave em segredo.

Manter o algoritmo em segredo é muitas vezes impraticável, porque você precisaria alterar algoritmos cada vez que alguém vazasse o algoritmo. Com um algoritmo publico, a vantagem é que uma chaveusada por um algoritmo para controlar o processo de criptografia. Assim, os cuidados em relação a criptografia é igual a uma senha comum: a chave deve sermantida em segredo e não deve ser fácil adivinhar.

Outra vantagem de algoritmo público é que ele é extensivamente testado. Quando um sucessor do amplo algoritmo do padrão de criptografia de dados (DES) se tornou necessário, o Instituto Nacional Americano de Padrões e Tecnologia (NIST) convidou qualquer pessoa a enviar novos algoritmos. Após o encerramento do período de envio, o NIST tornou público o código-fonte desses algoritmos e convidou todos a quebrá-los. Alguns algoritmos foram quebrados em questão de dias, e apenas um pequeno número chegou à rodada final. Tornar esses algoritmos públicos melhorou a segurança.

Escolhendo um algoritmo de criptografia apropriado

Você viu que a criptografia e a descriptografia protegem os dados contra acesso não autorizado. Existem dois tipos de criptografia que têm adequação própria para uso de acordo com dois cenários. Esses são:
1.	Criptografia simétrica
2.	Criptografia assimétrica

Uma maneira é chamada de criptografia simétrica, ou segredo compartilhado (shared secret), e a segunda é chamada de criptografia assimétrica ou chave pública. Os dois tipos de criptografia são descritos em mais detalhes a seguir.

A Microsoft implementou alguns dos algoritmos existentes no .NET, que são implementados de três maneiras:
•	Classes gerenciadas (no .NET): o nome da classe para essas é o nome do algoritmo com o sufixo Managed, por exemplo, RijndaelManaged é a classe gerenciada que implementa o algoritmo Rijndael. As implementações gerenciadas são um pouco mais lentas que as outras implementações e não são certificadas pelo Federal Information Processing Standards (FIPS).
•	Classes de wrapper em torno da implementação nativa da API de criptografia (CAPI): O nome da classe para esses é o nome do algoritmo com o sufixo CryptoServiceProvider, por exemplo DESCryptoServiceProvider é a classe de wrapper que implementa o algoritmo Data Encryption Standard (DES). As implementações CAPI são adequadas para sistemas mais antigos, mas não estão mais sendo desenvolvidas.
•	Classes de wrapper em torno da implementação da API Cryptography Next Generation (CNG) nativa: O nome da classe para esses é o nome do algoritmo com sufixo CNG, por exemplo, ECDiffieHellmanCng é a classe de wrapper que implementa o algoritmo Elliptic Curve Diffie-Hellman (ECDH). Os algoritmos de CNG requerem um Windows Vista ou um sistema operacional mais recente. 

Todas as classes de criptografia são definidas no espaço para nome System.Security.Cryptography e fazem parte da biblioteca .NET principal. O .Net disponibiliza vários algoritmos para executar criptografia ou hash, é importante escolher o melhor algoritmo com relação ao cenário. Os pontos a seguir ilustram o uso de diferentes algoritmos comumente usados em relação ao cenário.
•	Dados Mais Confidenciais, use a Criptografia Assimétrica em vez da criptografia simétrica.
•	Privacidade de Dados, use Aes (Algoritmo Smétrico).
•	Integridade de Dados, use os algoritmos de hash HMACSHA256 e HMACSHA512.
•	Assinatura Digital, use ECDsa e RSA.
•	Gerar um Número Aleatório, use RNGCryptoServiceProvider.
•	Troca de chaves, use RSA ou ECDiffieHellman.
•	Gerar uma chave a partir de uma senha, use Rfc2898DeriveBytes


Criptografia Simétrica

A diferença nas estratégias de criptografia simétrica e assimétrica está na maneira como essa chave da criptografia publica é usada. Na criptografia simétrica usa uma única chave para criptografar e descriptografar os dados. Você precisa passar sua chave original para o receptor para que ele possa descriptografar seus dados.Também é chamado de criptografia secreta compartilhada (shared secret). E isso automaticamente leva ao problema da troca segura de chaves.

Os dados são seguros devido à criptografia simétrica, mas devem ser enviados a uma pessoa autorizada, pois a chave também viaja com os dados. Quando os dados são enviados para uma pessoa não autorizada, os dados ficam comprometidos, pois o receptor pode descriptografar os dados com a chave recebida.

O algoritmo para criptografia simétrica funciona da seguinte maneira: os dados a serem criptografados são transformados em blocos de cifra e cada bloco tem um tamanho específico para conter dados cifrados. Isso é chamado de encadeamento de blocos de cifras Quando os dados são maiores que o tamanho do bloco (tamanho do bloco), os dados são divididos em vários blocos. O tamanho do bloco depende do algoritmo usado.

O primeiro bloco contém valor criptografado de algum valor aleatório chamado Vetor de Inicialização (IV) e chave de criptografia, o próximo bloco contém valor criptografado do bloco anterior com chave e assim por diante. Se o tamanho do último bloco for menor do que os dados, o bloco será preenchido. O algoritmo simétrico é mais rápido que a criptografia assimétrica e adequado para grande quantidade de dados.

O .NET Framework fornece cinco algoritmos simétricos diferentes para trabalhar.Esses algoritmos simétricos são definidos no .NET e podem ser encontradas nas classes System.Security.Cryptography
Algoritmo	Descrição
AES	Advanced Encryption Standard é um algoritmo simétrico. Foi projetado para ambos software e hardware. Ele suporta dados de 128 bits e chave de 128.192.256 bits. Há duas classes implementando esse algoritmo: AesManaged e AesCryptoServiceProvider
DES	Data Encryption Standard implementado por DESCryptoServiceProvider é um algoritmo simétrico publicado pelo Instituto Nacionalde Padrão e Tecnologia (NIST).
RC2	Ron’s code ou Rivest Cipher também conhecido como ARC2 é um algoritmo simétrico projetado de Ron Rivest implementado por RC2CryptoServiceProvider
Rijndael	Rijndael é um algoritmo simétrico escolhido pela NSA como um padrão de criptografia avançada (AHS). implementado por RijndaelManaged
TripleDes	TripleDes também conhecido como 3DES (Triple Data Encryption Slandard) aplica algoritmo DES três vezes para cada bloco de dados implementado pelo TripleDESCryptoServiceProvider

Por exemplo, temos um dado secreto: "Mensagem Secreta" e queremos criptografá-lo. Você pode usar qualquer um dos algoritmos acima (classes), todas herdam da classe System.Security.Cryptography.SymmetricAlgorithm. Esta classe contém propriedades e métodos que são úteis ao trabalhar com algoritmos simétricos. As tabelas abaixo listam as propriedades e métodos dessa classe.
Propriedade	Descrição
BlockSize	Pega ou define o tamanho do bloco usado pela operação criptográfica. O tamanho do bloco é especificado em bits e representa a unidade básica de dados que pode ser criptografada ou descriptografada em uma operação. Mensagens maiores que o tamanho do bloco são divididas em blocos desse tamanho; mensagens menores que o tamanho do bloco são preenchidas com bits extras até atingirem o tamanho de um bloco O algoritmo usado determina a validade do tamanho do bloco
FeedbackSize	Obtém ou define o tamanho do tamanho do feedback usado pela operação criptográfica. O tamanho do feedback representa a quantidade de dados em bits que é retornada para a próxima operação de criptografia ou descriptografia. O tamanho do feedback deve ser menor que o tamanho do bloco.
IV	Obtém ou define o IV. Sempre que você cria uma nova instância de um algoritmo simétrico, o IV é definido como um novo valor aleatório. Você também pode gerar um chamando o método GenerateIV. O tamanho da propriedade IV deve ser o mesmo que a propriedade BlockSize dividida por oito.
Key	Obtém ou define a chave secreta usada pelo algoritmo. A chave secreta deve ser a mesma para criptografia e descriptografia. Para que um algoritmo simétrico seja bem-sucedido, a chave secreta deve ser mantida em segredo. Os tamanhos de chave válidos são especificados pela implementação específica do algoritmo simétrico
e estão listados na propriedade LegalKeySizes.
KeySize	Obtém ou define o tamanho da chave secreta usada pelo algoritmo simétrico.Os tamanhos de chave válidos são especificados em bits pela implementação específica do algoritmo simétrico e são listados na propriedade LegalKeySizes.
LegalBlockSizes	Obtém os tamanhos dos blocos em bits que são aceitos pelo algoritmo.
LegalKeySizes	Obtém os tamanhos das chaves em bits que são aceitos pelo algoritmo.
Mode	Obtém ou define o modo de operação do algoritmo. Consulte a enumeração System.Security.Cryptography.CipherMode para obter uma descrição dos modos específicos.
Padding	Obtém ou define o modo de preenchimento usado pelo algoritmo. Consulte a enumeração System.Security.Cryptography.PaddingMode para obter uma descrição dos modos específicos.


Metodo	Descrição
Clear	Libera todos os recursos usados pela classe SymmetricAlgorithm. É necessário chamar esse método para limpar todos os recursos alocados pelo algoritmo para garantir que nenhum dado sensível permaneça na memória quando você terminar o objeto criptográfico. Não confie no coletor de lixo para limpar os dados.
Create()	Esse método estático cria uma nova criptografia usando o algoritmo padrão, que no .NET 4.5 é RijndaelManaged.
Create(String)	Create (String) Este método estático cria um novo objeto criptográfico usando o algoritmo criptográfico especificado. O nome do algoritmo pode ser um dos nomes na coluna esquerda da tabela de Algoritmo acima ou o nome do próprio tipo, com ou sem o espaço para nome. Aes corresponde ao algoritmo AesCryptoServiceProvider. Se você quiser usar a versão gerenciada do algoritmo, precisará especificar AesManaged.
CreateDecryptor()	Cria um objeto de decodificador usando a Chave e IV atualmente definidas nas propriedades.
CreateDecryptor (Byte[], Byte[])	Cria um objeto de decodificador usando os valores Key e IV especificados como parâmetros.
CreateEncryptor()	Cria um objeto criptografador usando a Chave e IV atualmente definidas nas propriedades.
CreateEncryptor (Byte[], Byte[])	Cria um objeto criptografador usando as propriedades Key e IV especificadas como parâmetros.
GenerateIV	Gera um IV aleatório para ser usado pelo algoritmo. Normalmente não há necessidade de chamar esse método.
GenerateKey	Gera uma chave aleatória a ser usada pelo algoritmo. A chave secreta deve ser a mesma para criptografia e descriptografia. Para que um algoritmo simétrico seja bem-sucedido, a chave secreta deve ser mantida em segredo. Os tamanhos de chave válidos são especificados pela implementação específica do algoritmo simétrico e estão listados na propriedade LegalKeySizes.
ValidKeySize	Retorna true se o tamanho da chave especificado for válido para este algoritmo específico.

O fluxo de trabalho da criptografia de texto sem formatação no texto do picador é direto:
1.	Crie um objeto de algoritmo simétrico chamando o método Create da classe SymmetricAlgorithm, configurando o parâmetro opcional string para o nome do algoritmo desejado.
2.	Se desejar, você pode definir uma chave e um IV, mas isso não é necessário porque eles são gerados por padrão.
3.	Crie um objeto criptografador chamando o método CreateEncryptor. Novamente, você pode optar por enviar a chave e o IV como parâmetros para esse método ou usar o padrão gerado.
4.	Chame o método TransformFinalBlock no criptografador, que recebe como entrada uma matriz de bytes, representando os dados simples, o deslocamento de onde iniciar a criptografia e o comprimento dos dados a serem criptografados. Retorna os dados criptografados de volta.

byte[] EncryptData(byte[] plainData, byte[] IV, byte[] key)
{
    // 1.Crie um objeto de criptografia
    SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

    // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

    // 3.Crie um objeto criptografador
    ICryptoTransform encryptor = cryptoAlgorythm.CreateEncryptor(key, IV);

    //4.Chame o método TransformFinalBlock no criptografador
    byte[] cipherData = encryptor.TransformFinalBlock(plainData, 0,
    plainData.Length);
    return cipherData;
}

O fluxo de trabalho de descriptografar o texto para recuperar o texto sem formatação também é simples:
1.	Crie um objeto de algoritmo simétrico chamando o método Create da classe SymmetricAlgorithm, definindo o parâmetro opcional string como o nome do mesmo algoritmo usado para criptografia.
2.	Se desejar, você pode definir uma chave e um IV, mas isso não é necessário agora, porque você pode defini-los na próxima etapa.
3.	Crie um objeto de decodificador chamando o método CreateDecryptor. Agora você deve definir a chave e o IV enviando-os como parâmetros para esse método, se não o fez na etapa anterior. A chave e o IV devem ser os mesmos que os usados para criptografia.
4.	Chame o método TransformFinalBlock no decodificador, que recebe como entrada uma matriz de bytes, que são os dados do chipper, o deslocamento de onde iniciar a descriptografia e o comprimento dos dados a serem descriptografados e retorna os dados simples.

byte[] DecryptData(byte[] cipherData, byte[] IV, byte[] key)
{
    // 1.Crie um objeto de criptografia
    SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

    // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

    // 3.Crie um objeto criptografador
    ICryptoTransform decryptor = cryptoAlgorythm.CreateDecryptor(key, IV);

    //4.Chame o método TransformFinalBlock no criptografador
    byte[] plainData = decryptor.TransformFinalBlock(cipherData, 0,
    cipherData.Length);
    return plainData;
}

O algoritmo e a chave usados para criptografia devem ser os mesmos durante a descriptografia. Os dados devem estar em bytes, pois o System.Security.Cryptography funciona em bytes de dados para criptografar.

Você chama gerar uma chave e IV de duas maneiras. A primeira abordagem é permitir que o usuário defina a chave e o IV. e isso é possível atribuindo um valor às propriedades Key e IV da classe Symmetric Algorithm. A principal desvantagem dessa abordagem é que ela é muito suscetível a ataques de força bruta, e como os seres humanos são fracos quando se trata de cunhar uma mensagem que é verdadeiramente única e de natureza aleatória. 

aesAlg.Key = newbyte[32] { 118, 123, 23, 17, 161, 152, 35, 68, 126, 213, 16, 115, 68, 217, 58, 108, 56, 218, 5, 78, 28, 128, 113, 208, 61, 56, 10, 87, 187, 162, 233, 38 };
aesAlg.IV = newbyte[16] { 33, 241, 14, 16, 103, 18, 14, 248, 4, 54, 18, 5, 60, 76, 16, 191 };
ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

A outra abordagem é confiar no provedor de algoritmo subjacente para produzir uma chave automaticamente. Por aqui. uma chave forte é gerada e difícil de adivinhar.

SymmetricAlgorithm symmetric = SymmetricAlgorithm.Create();
symmetric.GenerateIV();
symmetric.GenerateKey();

A classe SymmetricAlgorithm é uma classe abstrata de algoritmos simétricos (AES, DES, etc.). Você pode usar o método Create para criar o objeto padrão para criptografia. Por padrão, ele usa um algoritmo RijndaelManaged (uma versão gerenciada do algoritmo Rijndael). Você pode dar o nome de qualquer algoritmo simétrico no método Create ou criar a instância deles. Depois de especificar o algoritmo, você especifica a chave e IV (que são opcionais) e cria o criptografador. TransformFinalBlock é usado para transformar dados em bytes em texto cifrado.

Para descriptografar dados, crie o decodificador e chame a mesma função no texto cifrado criado em TransformFinalBlock. O .NET Framework oferece um amplo conjunto de algoritmos para criptografia simétrica e assimétrica. Um algoritmo simétrico muito utilizado é o Advanced Encryption Standard (AES). A AES é adotada pelo governo dos EUA e está se tornando o padrão mundial para uso governamental e comercial. O .NET Framework possui uma implementação gerenciada do algoritmo AES na classe AesManaged. 

O código abaixo mostra um exemplo de uso desse algoritmo para criptografar e descriptografar uma parte do texto. Como você pode ver, o AES é um algoritmo simétrico que usa uma chave e IV para criptografia. Usando a mesma chave e IV, você pode descriptografar um pedaço de texto. Todas as classes de criptografia funcionam em seqüências de bytes.

static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
{
    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
    using (MemoryStream msEncrypt = new MemoryStream())
    {
        using (CryptoStream csEncrypt =
        new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            return msEncrypt.ToArray();
        }
    }
}

static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
{
    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
    {
        using (CryptoStream csDecrypt =
        new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        {
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}

string original = "My secret data!";
using (SymmetricAlgorithm symmetricAlgorithm = new AesManaged())
{
    byte[] encrypted = Encrypt(symmetricAlgorithm, original);
    string roundtrip = Decrypt(symmetricAlgorithm, encrypted);

    Console.WriteLine("Original: {0}", original);
    Console.WriteLine("Round Trip: {0}", roundtrip);
}

Classe CryptoStream

Conforme discutido no Capítulo 9, “Trabalhando com dados”, os fluxos representam um arquivo, um dispositivo de E / S ou um canal de comunicação e podem executar três operações fundamentais:
•	Leitura (Read) : você pode transferir dados do fluxo para uma estrutura de dados.
•	Gravação (Write) : você pode transferir dados para o fluxo a partir de uma estrutura de dados.
•	Procurar (Seek) : Você pode alterar a posição atual no fluxo onde a próxima leitura ou gravação operação opera.

Uma propriedade importante dos fluxos no .NET é que eles podem ser encadeados, alimentando os dados de saída de um fluxo na entrada de outro fluxo. Às vezes, você pode querer criptografar os dados que passam por esses fluxos, a fim de garantir a privacidade ou a integridade dos dados. Você pode criptografar os dados antes de serem enviados pelo fluxo ou pode encadear os fluxos para que um deles seja responsável pela criptografia.

A classe SymmetricAlgorithm possui um método para criar um criptografador e um decodificador. Usando a classe CryptoStream, você pode criptografar ou descriptografar uma sequência de bytes. A criptografia simétrica pode ser usada também para criptografar Streams. O fluxo de trabalho dos fluxos de criptografia é direto:
1.	Crie um objeto de algoritmo simétrico chamando o método Create da classe SymmetricAlgorithm, configurando o parâmetro opcional string para o nome do algoritmo desejado.
2.	Se desejar, você pode definir uma chave e um IV, mas isso não é necessário porque eles serão gerados por padrão.
3.	Crie um objeto criptografador chamando o método CreateEncryptor. Novamente, você pode optar por enviar a chave e o IV como parâmetros para esse método ou usar o padrão gerado.
4.	Crie os fluxos MemoryStream usados para criptografia.
5.	Crie um objeto CryptoStream. O construtor do CryptoStream espera três parâmetros. O primeiro parâmetro é o fluxo para o qual você envia os dados criptografados; o segundo é o criptografador que você criou na etapa anterior; e o terceiro é o modo de operação de fluxo, que neste caso é gravação.
6.	Grave dados no objeto CryptoStream chamando um dos métodos de gravação expostos pelo CryptoStream, usando um StreamWriter ou encadeando-os para outro fluxo.
7.	Limpe o objeto CryptoStream de todos os dados confidenciais chamando o método Clear e descarte o objeto.

O código deve ficar assim:

byte[] EncryptString(string plainData, byte[] IV, byte[] key)
{
    //1.Crie um objeto de algoritmo simétrico
    SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

    // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

    //3.Crie um objeto criptografador chamando o método CreateEncryptor
    ICryptoTransform encryptor = cryptoAlgorythm.CreateEncryptor(key, IV);
    byte[] cipherData = new byte[0];

    // 4. Crie os fluxos usados para criptografia.
    using (MemoryStream msEncrypt = new MemoryStream())
    {
        //5.Crie um objeto CryptoStream
        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            //6.Grave dados no objeto CryptoStream usando um StreamWriter ou thread 
            StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(plainData);

            //7.Limpe o objeto CryptoStream chamando o método Clear e descarte o objeto.
            swEncrypt.Close();
            csEncrypt.Clear();
            cipherData = msEncrypt.ToArray();
        }
    }
    return cipherData;
}

O fluxo de trabalho da descriptografia de fluxos também é direto:
1.	Crie um objeto de algoritmo simétrico chamando o método Create da classe SymmetricAlgorithm, configurando o parâmetro opcional string para o nome do mesmo algoritmo usado para criptografia.
2.	Se desejar, você pode definir uma chave e um IV, mas isso não é necessário agora, porque você pode defini-los na próxima etapa.
3.	Crie um objeto de decodificador chamando o método CreateDecryptor. Agora você precisa definir a chave e o IV enviando-os como parâmetros para esse método, se não o fez na etapa anterior. A chave e o IV devem ser os mesmos que os usados para criptografia.
4.	Crie os fluxos MemoryStream usados para criptografia.
5.	Crie um objeto CryptoStream. O construtor do CryptoStream espera três parâmetros. O primeiro parâmetro é o fluxo para onde enviar os dados criptografados; o segundo é o decodificador que você criou na etapa anterior; e o terceiro é o modo de operação de fluxo, que neste caso é Read.
6.	Leia os dados do objeto CryptoStream chamando um dos métodos Read expostos por CryptoStream, usando um StreamReader ou encadeando-o para outro fluxo.
7.	Limpe o objeto CryptoStream de todos os dados confidenciais chamando o método Clear e descarte o objeto.

O código deve ficar assim:

string DecryptString(byte[] cipherData, byte[] IV, byte[] key)
{
    //1.Crie um objeto de algoritmo simétrico
    SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

    // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

    //3.Crie um objeto criptografador chamando o método CreateEncryptor
    ICryptoTransform decryptor = cryptoAlgorythm.CreateDecryptor(key, IV);
    string plainText = string.Empty;

    // 4. Crie os fluxos usados para criptografia.
    using (MemoryStream msDecrypt = new MemoryStream(cipherData))
    {
        //5.Crie um objeto CryptoStream
        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        {
            //6.Leia os dados do objeto CryptoStream usando um StreamReader ou thread 
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            plainText = srDecrypt.ReadToEnd();

            //7.Limpe o objeto CryptoStream chamando o método Clear e descarte o objeto.
            srDecrypt.Close();
            csDecrypt.Clear();
        }
    }
    return plainText;
}

Um cenário comum é criptografar e compactar os dados que são enviados via rede. A ordem em que você faz isso é importante por dois motivos. Primeiro, compactar texto é mais eficaz do que compactar dados binários, resultando em menos dados para criptografar. Segundo, você deve aplicar as transformações na ordem inversa, o que significa que, se você compactar primeiro e depois criptografar os dados no lado do remetente, deverá primeiro descriptografar os dados e descompactá-los no lado do receptor. Abaixo uma implementação completa de encriptografia e decriptografia utilizando CriptoStream.

class Criptografia_Stream
{
    public byte[] EncryptStream(SymmetricAlgorithm symmetricAlgo)
    {
        // especifique os dados
        string plainData = "Mensagem Secreta STREAM";

        byte[] cipherDataInBytes;

        ICryptoTransform encryptor = symmetricAlgo.CreateEncryptor(symmetricAlgo.Key, symmetricAlgo.IV);

        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (CryptoStream crptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(crptoStream))
                {
                    streamWriter.Write(plainData);
                }

                cipherDataInBytes = memoryStream.ToArray();

                string cipherData = Encoding.UTF8.GetString(cipherDataInBytes);

                Console.WriteLine("Stream criptografado:" + cipherData);
            }
        }

        return cipherDataInBytes;
    }

    public void DecryptStream(SymmetricAlgorithm symmetricAlgo, byte[] cipherDataInBytes)
    {
        ICryptoTransform decryptor = symmetricAlgo.CreateDecryptor(symmetricAlgo.Key, symmetricAlgo.IV);

        using (MemoryStream msDecrypt = new MemoryStream(cipherDataInBytes))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    var plaintext = srDecrypt.ReadToEnd();

                    Console.WriteLine("Stream Descriptografado..." + plaintext);
                }
            }
        }
    }
}

using (SymmetricAlgorithm symmetricAlgo = SymmetricAlgorithm.Create())
{
    var stream_cripto = new Criptografia_Stream();

    var cipherDataInBytes = stream_cripto.EncryptStream(symmetricAlgo);
    stream_cripto.DecryptStream(symmetricAlgo, cipherDataInBytes);
}

Criptografia Assimétrica

A criptografia assimétrica usa um par de duas chaves em vez de uma para criptografia. Essas duas chaves são matematicamente relacionadas uma à outra. Uma das chaves é chamada chave pública e outra é chamada chave privada. Os dados são criptografados com a chave pública do destinatário e só podem ser descriptografados pela chave privada do destinatário específico, pois somente esse usuário deve ter acesso à chave privada. A chave pública transmite os dados enquanto a chave secreta é mantida com o destinatário. A outra chave deve ser do par de chaves que você gerou. A criptografia que você faz com essas chaves é intercambiável. Por exemplo, se key1 criptografa os dados, então key2 pode descriptografá-los e se key2 criptografa os dados, então key1 pode descriptografá-los, porque um deles pode ser dado a todos e o outro deve ser mantido em segredo.

As chaves privadas assimétricas nunca devem ser armazenadas no formato textual nem como texto sem formatação no computador local. Se precisar armazenar uma chave privada, você deverá usar um contêiner de chave. A criptografia assimétrica evita o compartilhamento da chave de criptografia; é por isso que é mais seguro que uma chave simétrica. Mas, por outro lado, é mais lento que a criptografia simétrica. O .NET Framework fornece vários algoritmos assimétricos para trabalhar. As classes dos algoritmos assimétricos também podem ser encontradas emSystem.Security.Cryptography
Algoritmo	Descrição
RSA	Desenvolvido por (Rivest-Shamir-Adleman) é um algoritmo assimétrico implementado pelo RSACryptoServiceProvider  comumente usado por computadores modernos.
DSA	Digital Signature Algorithm, produzido pela NISI, é um padrão para criar assinaturas digitais para integridade dos dados. Algoritmo de assinatura digital DSA. Usado para criar assinaturas digitais que ajudam a proteger a integridade dos dados. Há uma classe implementando esse algoritmo: DSACryptoServiceProvider. Use o DSA apenas para compatibilidade com aplicativos e dados herdados.
ECDsa	Elliptic Curve Digital Signature  oferece uma variante do DAS implementado por ECDsaCng.
ECDiffieHellman	O algoritmo Elliptic Curve Diffie-Hellman implementado por ECDiffieHellmanCng. Fornece um conjunto básico de operações que suportam as implementações do ECD.

No .NET, todas as classes que implementam um algoritmo assimétrico são herdadas de System.Security.Cryptography.AsymmetricAlgorithm. O fluxo de trabalho de criptografia de dados usando criptografia assimétrica segue:
1.	Crie um objeto de criptografia assimétrica
2.	Gerando a chave pública do receptor.
3.	Defina a chave pública.
4.	Criptografe os dados.
5.	Envie os dados para o receptor.
6.	Limpe o objeto de criptografia assimétrica de todos os dados confidenciais chamando o método Clear e descarte o objeto.

//1.Crie um objeto de criptografia assimétrica da classe System.Security.Cryptography 
RSACryptoServiceProvider asymmetricAlgo = new RSACryptoServiceProvider(parameter);

// salvando as informações principais na estrutura RSAParameters
RSAParameters RSAKeyInfo = asymmetricAlgo.ExportParameters(false);
//2 - gerando as duas chaves (pública e privada)
string publicKey = asymmetricAlgo.ToXmlString(false);
string privateKey = asymmetricAlgo.ToXmlString(true);

//Opção para coversão de texto para bytes
UnicodeEncoding ByteConverter = new UnicodeEncoding();
byte[] dataToEncrypt = ByteConverter.GetBytes("My Secret Data!");

// Código de criptografia (no lado do remetente) dados para criptografar
string dados = "Mensagem Secreta";

// converte em bytes
byte[] dataInBytes = Encoding.UTF8.GetBytes(dados);

// 3 - Especifique a chave pública obtida do receptor
rsa.FromXmlString(publicKey);

// 4 - Use o método Encrypt para criptografia
byte[] encryptedDataInBytes = asymmetricAlgo.Encrypt(dataInBytes, true);

// 5.Envie os dados para o receptor.
string encryptedData = Encoding.UTF8.GetString(encryptedDataInBytes);

//6.Limpe o objeto de criptografia assimétrica de todos os dados confidenciais chamando o método Clear e descarte o objeto.
asymmetricAlgo.PersistKeyInCsp = false;
asymmetricAlgo.Clear();

O fluxo de trabalho de descriptografar dados usando criptografia assimétrica segue:
1.	Crie um objeto de criptografia assimétrica
2.	Obtenha a chave privada do receptor.
3.	Obtenha os dados do remetente.
4.	Especifique a chave privada
5.	Descriptografe os dados.
6.	Limpe o objeto de criptografia assimétrica de todos os dados confidenciais chamando o método Clear e descarte o objeto.

//1.Crie um objeto de criptografia assimétrica da classe System.Security.Cryptography 
RSACryptoServiceProvider asymmetricAlgo = new RSACryptoServiceProvider(parameter)

// salvando as informações principais na estrutura RSAParameters
RSAParameters RSAKeyInfo = asymmetricAlgo.ExportParameters(false);
// 2 - gerando as duas chaves (pública e privada)
string publicKey = asymmetricAlgo.ToXmlString(false);
string privateKey = asymmetricAlgo.ToXmlString(true);

//3.Obtenha os dados do remetente criptografados
byte[] encryptedDataInBytes = asymmetricAlgo.Encrypt(dataInBytes, true);

// 4 - Especifique a chave privada
asymmetricAlgo.FromXmlString(privateKey);

// 5 - Use o método Decrypt para criptografia
byte[] decryptedDataInBytes = asymmetricAlgo.Decrypt(encryptedDataInBytes, true);

// coloca os bytes dos dados descriptografados na string
string decryptedData = Encoding.UTF8.GetString(decryptedDataInBytes);

//6.Limpe o objeto de criptografia assimétrica de todos os dados confidenciais chamando o método Clear e descarte o objeto.
asymmetricAlgo.PersistKeyInCsp = false;
asymmetricAlgo.Clear();

O método ToXmlString retorna a chave pública ou privada com base no valor booleano. Para gerar uma chave privada, torne o valor verdadeiro e, para uma chave pública, o valor deve ser falso. Agora, temos duas chaves interligadas de um algoritmo assimétrico. Se A deseja enviar dados para B, ambas as partes devem ter um entendimento sobre o padrão ou as chaves usadas para a comunicação entre elas. O destinatário (B) deve ter a chave privada para descriptografia e o remetente (A) criptografará os dados usando a chave pública. Os dados que viajaram para B serão descriptografados apenas com a chave secreta gerada junto com a chave pública (usada para criptografia). Se os dados foram alterados ou não foram criptografados usando a chave pública correspondente, um CryptographicException será lançada. Por xemplo, se invertermos e usarmos a chave privada para criptografar e a privada para descriptografar lança o erro:
 

A chave pública é a parte que você deseja publicar para que outras pessoas possam usá-la para criptografar dados. Você pode enviá-lo diretamente a alguém ou publicá-lo em um site que pertence a você. Por esse motivo, é importante armazenar a chave privada em um local seguro. Se você o armazenasse em texto sem formatação no disco ou mesmo em um local de memória não seguro, sua chave privada poderia ser extraída e sua segurança seria comprometida.

O .NET Framework oferece um local seguro para armazenar chaves assimétricas em um contêiner de chaves. Um contêiner de chaves pode ser específico para um usuário ou para toda a máquina. O exemplo a identificação do container foi definido pelos parâmetros setados em CspParameters e passados ao configurar um RSACryptoServiceProvider.

CspParameters parameter = new CspParameters();
parameter.KeyContainerName = "KeyContainer";
OU
CspParameters csp = new CspParameters() { KeyContainerName = "KeyContainer"};
using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(csp))

Carregar a chave do contêiner de chaves é exatamente o mesmo processo. Você pode armazenar com segurança sua chave assimétrica sem que usuários mal-intencionados possam lê-la.

Criptografia Simétrica e Assimétrica

	Além do tipo de chaves, outra diferença entre criptografia simétrica e assimétrica tem a ver com desempenho e tamanho da mensagem. A criptografia simétrica é mais rápida que a criptografia assimétrica e é adequada para conjuntos de dados maiores. A criptografia assimétrica não é otimizada para criptografar mensagens longas, mas pode ser muito útil para descriptografar uma chave pequena. Combinar um algoritmo simétrico e um assimétrico pode ser mais seguro e ajudá-lo a transmitir uma quantidade maior de dados de maneira criptografada. Um cenário comum é usar a criptografia assimétrica para trocar as chaves necessárias para a criptografia simétrica. Digamos que Bob e Alice querem enviar uma mensagem um ao outro. Eles seguem os seguintes passos:
1.	Alice e Bob geram seu próprio par de chaves assimétricas pública/privada.
2.	Eles enviam sua chave pública e mantêm sua chave privada em segredo.
3.	Ambos geram uma chave simétrica e a criptografam usando um algoritmo assimétrico apenas com a chave pública das outras partes (para que possa ser criptografada pela chave privada da outra pessoa).
4.	Eles enviam suas próprias chaves simétricas criptografadas e descriptografam as outras chaves simétricas com sua própria chave privada.
5.	Para enviar uma mensagem confidencial, eles usam a chave simétrica da outra pessoa e a criptografam.
6.	Cada parte descriptografa a chave usando o mesmo algoritmo assimétrico e sua própria chave privada para obter a chave simétrica gerada pela outra parte.
7.	Eles começam a trocar dados usando o mesmo algoritmo simétrico e as chaves do passo anterior.

Como você pode ver, a criptografia assimétrica é usada para criptografar uma chave simétrica. Depois que a chave é transmitida com segurança, Bob e Alice podem usá-la para enviar mensagens maiores um para a outro.

Além de usar uma chave, outro conceito importante em criptografia é o vetor de inicialização (IV). Um IV é usado para adicionar alguma aleatoriedade aos dados criptografados. Se a criptografia do mesmo texto sempre fornecer os mesmos resultados, isso poderá ser usado por um invasor em potencial para interromper a criptografia. O IV garante que os mesmos dados resultem em uma mensagem criptografada diferente a cada vez.

Outro cenário comum é usar a criptografia assimétrica para assinar dados digitalmente, garantindo dessa maneira a autenticidade e a identidade.

Classe ProtectedData 

Sem se preocupar em usar o algoritmo de criptografia (ou seja, simétrico ou assimétrico), você ainda pode proteger seus dados usando a classe ProtectedData. 

A classe ProtectedData faz parte do namespace System.Security.Cryptography. Para adicionar-la em um projeto, você deve adicionar também o assembly System.Security na pasta de referências. Ela contém dois métodos estáticos:
1.	Protect(): é o método estático da classe ProtectedData; é usado para criptografar os dados. Ele contém a seguinte assinatura de método:

public static byte[] Protect( byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)

2.	Unprotect(): é o método estático da classe ProtectedData; é usado para descriptografar os dados criptografados. Ele contém a seguinte assinatura de método:

public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)

A assinatura do método dos métodos Proteger e Desproteger são os mesmos, ambos os métodos aceitam três parâmetros.
Parâmetros	Descrição
userData	Uma matriz de bytes que contém dados e representa os dados por criptografado (userData) ou descriptografado (encryptedData)
optionalEntropy	É uma matriz de bytes opcional usada para aumentar a complexidade da criptografia ou nula para nenhuma complexidade adicional.
scope	Assume o valor da enumeração DataProtectionScope que especifica o escopo da criptografia, quem pode descriptografar os dados
	CurrentUser  especifica-se que apenas o usuário atual pode descriptografar os dados criptografados
	LocalMachine especifica que qualquer usuário conectado na máquina local poderá descriptografar os dados

static void Main(string[] args)
{
    var protect_ocripto = new Criptografia_ProtectedData();

    var cipherDataInBytes = protect_ocripto.EncryptProtectedData();
    protect_ocripto.DecryptProtectedData(cipherDataInBytes);

    Console.ReadKey();
}

class Criptografia_ProtectedData
{
    public byte[] EncryptProtectedData()
    {
        string message = "Olá, mundo";
        // Converte dados em uma matriz de bytes
        byte[] userData = Encoding.UTF8.GetBytes(message);
        // criptografa os dados usando o método ProtectedData.Protect
        byte[] encryptedDataInBytes = ProtectedData.Protect(userData, null, DataProtectionScope.CurrentUser);
        string encryptedData = Encoding.UTF8.GetString(encryptedDataInBytes);
        Console.WriteLine("Criptografados por ProtectedData:" + encryptedData);
        return encryptedDataInBytes;
    }

    public void DecryptProtectedData(byte[] encryptedDataInBytes)
    {
        byte[] decryptedDataInBytes = ProtectedData.Unprotect(encryptedDataInBytes, null,
        DataProtectionScope.CurrentUser);
        string decryptedData = Encoding.UTF8.GetString(decryptedDataInBytes);
        Console.WriteLine("DeCriptografados por ProtectedData: " + decryptedData);
    }
}

Os dados da string são convertidos em uma matriz de bytes e, em seguida, o método Protect a criptografa, enquanto DataProtectionScope.CurrentUser especifica que apenas o usuário atual pode descriptografar os dados criptografados. DataProtectionScope é enumeração. CurrentUser significa que apenas o usuário atual pode criptografar os dados e LocalMachine significa que todos os usuários de uma máquina local podem criptografar dados

Criptografia de Arquivos 

O .NET Framework oferece uma maneira poderosa de proteger seus dados confidenciais. Ele fornece vários algoritmos que você pode usar no desenvolvimento de seu aplicativo.Ao trabalhar com arquivos, o objeto File fornece dois métodos quw  executam todas as atividades de criptografia e descriptografia em segundo plano...
•	Encrypt()
•	Descrypt()

Mesma funcionalidade (com mesma limitações e problemas) que as caixas de seleção nas propriedades do arquivo no Explorer e tem a criptografia é baseada na senha do usuário
 

class Criptografia_File
{
    public void ReadFile(string nome_arquivo)
    {
        int counter = 0;
        string line;

        // Read the file and display it line by line.
        System.IO.StreamReader file = new System.IO.StreamReader(nome_arquivo);
        while ((line = file.ReadLine()) != null)
            Console.WriteLine(line);
        counter++;

        file.Close();
    }

    public void EncryptFile(string nome_arquivo)
    {
        File.Encrypt(nome_arquivo);
        Console.WriteLine("Arquivo Criptografado ...");
    }

    public void DecryptFile(string nome_arquivo)
    {
        File.Decrypt(nome_arquivo);
        Console.WriteLine("Arquivo Descriptografado...");
    }
}

static void Main(string[] args)
{
    var cpt_file = new Criptografia_File();
    var nome_arquivo = @"ml.txt";

    Console.WriteLine("Press enter to encrypt the file...");
    Console.ReadLine();

    cpt_file.EncryptFile(nome_arquivo);
    cpt_file.ReadFile(nome_arquivo);

    Console.WriteLine("Press enter to decrypt the file...");
    Console.Read();

    cpt_file.DecryptFile(nome_arquivo);

    Console.ReadKey();
}

HASHING

Hashing é o processo de mapeamento de dados binários de comprimento variável para dados binários de tamanho curto e fixo. Esse processo é irreversível, ou seja, você não pode converter dados de hash de volta no original. A aplicação da mesma função de hash em duas estruturas de dados idênticas gera o mesmo resultado. As funções de hash são usadas em vários cenários:
•	Indexação de dados: em vez de corresponder os dados quando a chave de índice tiver um comprimento variável, calcule o valor do hash da chave de índice e localize-o. O valor do hash resultante de uma estrutura de dados geralmente é menor que o valor original; portanto, ao procurar uma quantidade menor de dados, o tempo de pesquisa também será menor. É possível que duas ou mais chaves de índice possam gerar o mesmo valor de hash. Nessa situação, o algoritmo de indexação usa uma técnica chamada hash bucket, onde todas as chaves de índice com o mesmo valor de hash são agrupadas. O tipo de hash usado nesse cenário não tem nada a ver com criptografia, mas vale a pena mencionar.
•	Integridade dos dados: a integridade dos dados é usada para garantir que os dados cheguem ao destino inalterados. O remetente calcula um hash criptográfico dos dados que deseja enviar e, em seguida, o remetente envia os dados, o hash e as informações sobre a técnica usada para calcular o hash para o destinatário. O receptor pode aplicar o mesmo algoritmo aos dados e comparará o hash resultante com o recebido. Se são iguais, significa que os dados não foram alterados após o cálculo do hash. No entanto, isso não garante que os dados não sejam alterados. Se alguém quiser alterar a mensagem, a única coisa que ela precisará fazer é calcular o hash da nova mensagem e enviá-la.
•	Autenticidade dos dados: a autenticidade dos dados é usada quando um receptor deseja garantir que os dados é proveniente do remetente certo e não é alterado a caminho. Funciona da seguinte maneira: O remetente calcula um hash criptográfico e o assina com sua própria chave privada. O destinatário faz o hash dos dados novamente e descriptografa a assinatura recebida, usa a chave pública dos remetentes para descriptografar a assinatura e verifica se é o mesmo que o hash.
•	Armazenamento de senhas: armazenar uma senha em texto sem formatação é uma técnica não segura e, se o armazenamento de dados for comprometido, todas as senhas também serão comprometidas. Para proteger as senhas, elas geralmente são hash e, em vez de salvar a senha, você salva o hash da senha. Quando alguém tenta fazer login, você pode fazer o hash da senha digitada e verificar se os dois hashes são os mesmos.

A força do hash criptográfico é que é improvável que duas entradas diferentes gerem o mesmo hash. Duas senhas que não são iguais e diferem pouco uma da outra podem produzir duas hashes completamente diferentes. Existem dois tipos de algoritmos de hash: com ou sem uma chave. Os algoritmos sem chaves são usados apenas para calcular hashes seguros de dados para garantir a integridade, enquanto os algoritmos codificados são usados juntos com uma chave como MAC para integridade e autenticidade.

Família de algoritmo	Descrição
Message Digest (MD)	Com diferentes versões como MD2, MD4 e a atual, denominada MD5: O tamanho do hash resultante para o MD5 é de 128 bits. Escolha esse algoritmo apenas se você trabalhar com aplicativos herdados. Use SHA256 ou SHA512, pois eles oferecem melhor segurança e desempenho. As implementações desse algoritmo devem herdar da classe abstrata System.Security.Cryptography.MD5. Existem duas implementações concretas no .NET 4.5: a implementação CAPI na classe MD5CryptoServiceProvider e a CNG na classe MD5Cng.
RACE Integrity Primitives Evaluation Message Digest (RIPEMD):	Tem tamanhos de hash resultantes de 128, 160, 256 e 320 bits. A versão implementada no .NET é aquela com o tamanho do hash de 160 bits. Escolha esse algoritmo apenas se você trabalhar com aplicativos herdados. Use SHA256 ou SHA512, pois eles oferecem melhor segurança e desempenho. Implementações desse algoritmo deve herdar da classe abstrata System.Security.Cryptography.RIPEMD160. Há apenas uma implementação desse algoritmo no .NET 4.5, a gerenciada na classe RIPEMD160Managed.
SHA-1	Éa segunda implementação do Secure Hash Algorithm desenvolvido pela Agência de Segurança Nacional (NSA). A primeira implementação foi chamada SHA-0, mas provou ter erros, que foram corrigidos pelo SHA-1. O tamanho do hash resultante para o SHA-1 é de 160 bits. Implementações deste algoritmo deve herdar da classe abstrata System.Security.Cryptography.SHA1. Existem três implementações concretas no .NET 4.5: a implementação gerenciada na classe SHA1Managed, a implementação CAPI na classe SHA1CryptoServiceProvider e a CNG na classe SHA1Cng.
SHA-2	É a terceira implementação do SHA pela NSA, que aborda algumas das vulnerabilidades encontradas no SHA-1. Essa família de algoritmos tem tamanhos de hash resultantes de 224, 256, 384 e 512 bits. O .NET não está implementando a versão de 224 bits do algoritmo. As implementações desse algoritmo devem herdar das classes abstratas de hash System.Security.Cryptography.SHA256 para o hash de tamanho de 256 bits, SHA384 para o hash de tamanho de 384 bits e SHA512 para o 512 de tamanho de bits. Todas as três versões são implementadas como wrappers CAPI gerenciados e wrappers de GNV.

O .NET Framework oferece algumas classes para gerar valores de hash. Os algoritmos que o .NET Framework em System.Security.Cryptography oferece são algoritmos de hash otimizados que produzem um código de hash significativamente diferente para uma pequena alteração nos dados.
Algoritmo	Descrição
SHA1	função que resulta em um valor de hash de 160 bits.
SHA256	função que resulta em um valor de hash de 256 bits.
SHA512	função que resulta em um valor de hash de 512 bits.
SHA384	função que resulta em um valor de hash de 384 bits.
MD5	Implementação do algoritmo de hash MD5
RIPEMD160 	(RACE Integrity Primitives Evalualion Message Digest) 160 é uma função de hash de criptografia, com desempenho semelhante ao SHA1.

Esses algoritmos (classes) são definidos no .NET e podem ser usados para executar o hash. Se você verificar a documentação online no MSDN para a classe HashAlgorithm, poderá observar três métodos extras não mencionados. Eles são HashCore, HashFinal e Initialize. Todos os três métodos são métodos abstratos que precisam ser implementados pelos implementadores do algoritmo de hash. É assim que a Microsoft implementou os algoritmos existentes. Embora pareça tentador, você não deve começar a implementar seus próprios algoritmos de hash se esse não for o seu negócio principal. Você deve usar as implementações existentes.
•	Quando houver um cenário para Integridade de Dados, use os algoritmos de hash HMACSHA256 e HMACSHA512.

Você pode usar qualquer um dos algoritmos de hash acima. Usamos o HMACSHA256 que é um algoritmo muito utilizado em cenário para Integridade de Dados e Armazenamento de Senhas, como veremos mais a frente um implentação completa. O fluxo de trabalho dos dados de hash é o seguinte:
1.	Crie um objeto de algoritmo de hash.
2.	Defina a chave de hash se o algoritmo usado for um com chave.
3.	Chame o método ComputeHash.
4.	Salve o hash dos dados.

static string ComputeHash(string input)
{
    //1.Crie um objeto de algoritmo de hash.
    HashAlgorithm hmac;

    //2.Defina a chave de hash se o algoritmo usado for um com chave.
    byte[] key1 = { 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
    hmac = new HMACSHA256(key1);

    //3.Chame o método ComputeHash.
    byte[] hashData = hmac.ComputeHash(Encoding.Default.GetBytes(input));

    //4.Salve o hash dos dados.
    return Convert.ToBase64String(hashData);
}

A seguir, o fluxo de trabalho de verificação de um hash para dados:
1.	Crie um objeto de algoritmo de hash usando o mesmo algoritmo usado para fazer hash dos dados.
2.	Se um algoritmo com chave de hash foi usado, defina a chave com o mesmo valor usado para o hash.
3.	Extraia o hash original dos dados.
4.	Chame o método ComputeHash.
5.	Compare o hash extraído com o calculado. Se forem iguais, significa que os dados não foram alterados.

static bool VerifyHash(string input, string Base64Hash)
{
    //1.Crie um objeto de algoritmo de hash usando o mesmo algoritmo 
    //usado para fazer hash dos dados.
    HashAlgorithm hmac;

    //2.Se um algoritmo com chave de hash foi usado, 
    //defina a chave com o mesmo valor usado para o hash.
    byte[] key1 = { 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
    hmac = new HMACSHA256(key1);

    //3.Extraia o hash original dos dados.
    var hashData = Base64Hash;

    //4.Chame o método ComputeHash.
    byte[] hashinput = hmac.ComputeHash(Encoding.Default.GetBytes(input));

    //5.Compare o hash extraído com o calculado. Se forem iguais, 
    //significa que os dados não foram alterados.
    return Convert.ToBase64String(hashinput) == hashData;
}

O Convert.ToBase64String transforma a matriz de bytes de entrada em uma base64 codificada por seqüência de caracteres. Não há necessidade de usar esta função. A única razão pela qual a usamos aqui é apenas para facilitar a comparação do resultado das funções de hash. A alternativa teria sido escrever uma função que compara duas matrizes se elas são iguais em tamanho e conteúdo utilizando o método SequenceEqual:

Console.WriteLine(hashA.SequenceEqual(hashC)); // Displays: true

Como você pode ver, cadeias diferentes fornecem um código hash diferente e a mesma cadeia fornece exatamente o mesmo código hash. Isso permite que você veja se uma string foi alterada comparando os códigos de hash. No .NET, todos os algoritmos de hash herdam da classe abstrata System.Security.Cryptography.HashAlgorithm. As tabelas abaixo listam as propriedades e métodos da classe HashAlgorithm.
Propriedade	Descrição
CanReuseTransform	Propriedade somente leitura que retorna true se a transformação atual puder ser reutilizada
CanTransformMultipleBlocks	Propriedade somente leitura que retorna true se vários blocos puderem ser transformados
Hash	Propriedade somente leitura que retorna o código de hash calculado
HashSize	Propriedade somente leitura que retorna, em bits, o tamanho do código hash calculado
InputBlockSize	Propriedade somente leitura que retorna o tamanho do bloco de entrada
OutputBlockSize	Obtém o tamanho do bloco de saída
Key
(classe KeyedHashAlgorithm)	Propriedade de leitura/gravação que representa a chave a ser usada pelo algoritmo de hash. Se você tentar alterar a chave após o início do hash, será lançada uma CryptographicException.

Método	Descrição
Clear	Libera todos os recursos usados pelo classe HashAlgorithm.
ComputeHash (Byte [])	Calcula o hash para a matriz a byte.
ComputeHash (Stream)	Calcula o hash para o objeto a Stream.
ComputeHash (Byte [], Int32, Int32)	Calcula o hash para uma região de uma matriz de bytes.
Create()	Esse método estático cria uma nova criptografia usando o algoritmo padrão, que no .NET 4.5 é SHA1.
Create (String)	Esse método estático cria um novo objeto de algoritmo de hash usando o algoritmo especificado. O nome do algoritmo pode ser um dos nomes na coluna esquerda da Tabela 12-6 ou o nome do próprio tipo, com ou sem o espaço para nome.
TransformBlock	Calcula o hash para a região especificada da matriz de bytes inputBuffer e copia o resultado para a região especificada da matriz de bytes outputBuffer.
TransformFinalBlock	Calcula o hash para a região especificada da matriz de bytes inputBuffer.

Normalmente, você usa apenas os métodos Create e ComputeHash. Os métodos TransformBlock e TransformFinalBlock são usados quando você deseja calcular o hash para partes de seus dados.

Exemplo Indexação de Dados

Para entender o que é hash e ver algumas das idéias por trás de um código de hash, dê uma olhada na classe definida abaixo. A classe Set armazena apenas itens exclusivos, então ela verifiaca se um item já existe antes de adicioná-lo.

class Set<T>
{
    private List<T> list = new List<T>();
    public void Insert(T item)
    {
        if (!Contains(item))
            list.Add(item);
    }
    public List<T> Itens()
    {
        return list;
    }
    public bool Contains(T item)
    {
        foreach (T member in list)
            if (member.Equals(item))
                return true;
        return false;
    }
}
setar.Insert("texto 1");
setar.Insert("texto 2");
setar.Insert("texto 1");
var valida = setar.Contains("texto 1");
Console.WriteLine(valida); //True
valida = setar.Contains("texto 3");
Console.WriteLine(valida); //False

foreach (var item in setar.Itens())
{
//texto 1
//texto 2
    Console.WriteLine(item); 
}

No método Contains(), ao adicionar um item, você precisa percorrer todos os itens existentes. Isso não é bem dimensionado e gera problemas de desempenho quando você tem uma grande quantidade de itens. Seria bom se você, de alguma forma, precisasse verificar apenas um pequeno subgrupo em vez de todos os itens.

É aqui que um código de hash pode ser usado. Hashing é o processo de pegar um grande conjunto de dados e mapeá-lo para um conjunto menor de dados de comprimento fixo. Por exemplo, mapeando todos os nomes para um número inteiro específico. Em vez de verificar o nome completo, você precisaria usar apenas um valor inteiro.

Usando o hash, você pode melhorar o design da classe Set do exemplo anterior. Você divide os dados em um conjunto de buckets. Cada bloco contém um subgrupo de todos os itens do conjunto, conforme implementado na classe SetHash abaixo.

class SetHash<T>
{
    private List<T>[] buckets = new List<T>[100];
    public void Insert(T item)
    {
        int bucket = GetBucket(item.GetHashCode());
        if (Contains(item, bucket))
            return;
        if (buckets[bucket] == null)
            buckets[bucket] = new List<T>();
        buckets[bucket].Add(item);
    }
    public Dictionary<int, string> Itens()
    {
        Dictionary<int, string> dict = new Dictionary<int, string>();
        for (int i = 0; i < 100; i++)
        {
            if (buckets[i] != null)
            {
                dict.Add(i, buckets[i][0].ToString().ToString());
            }
        }
        return dict;
    }
    public bool Contains(T item)
    {
        return Contains(item, GetBucket(item.GetHashCode()));
    }
    private int GetBucket(int hashcode)
    {
        // Um código Hash pode ser negativo. Para garantir que você  termine com um 
        // valor positivo, converta o valor para um int não assinado. O bloco unchecked 
        // garante que você possa converter um valor maior que int em int com segurança.
        unchecked
        {
            return (int)((uint)hashcode % (uint)buckets.Length);
        }
    }
    private bool Contains(T item, int bucket)
    {
        if (buckets[bucket] != null)
            foreach (T member in buckets[bucket])
                if (member.Equals(item))
                    return true;
        return false;
    }
}

var setahash = new SetHash<string>();

setahash.Insert("texto 1");
setahash.Insert("texto 2");
setahash.Insert("texto 1");
var validahash = setahash.Contains("texto 1");
Console.WriteLine(validahash); //True
validahash = setahash.Contains("texto 3");
Console.WriteLine(validahash); //False

foreach (var item in setahash.Itens())
{
    //70 - texto 1
    //97 - texto 2
    Console.WriteLine(string.Format("{0} - {1}", item.Key, item.Value));
}

Se você observar o método Contains, poderá ver que ele usa o método GetHashCode de cada item. Este método é definido na classe base Object. Em cada tipo (no exemplo foi utilizado string), você pode substituir esse método e fornecer uma implementação específica para o seu tipo. Este método deve gerar um código inteiro que descreve seu objeto específico. Como orientação geral, a distribuição dos códigos de hash deve ser a mais aleatória possível. É por isso que a implementação do conjunto usa o método GetHashCode em cada objeto para calcular em qual bucket ele deve ir.

O C# fornece o método GetHashCode() em todas as instâncias para gerar seu código de hash, que normalmente é usado para uma comparação de cadeia ou valor. Use o método GetHashCode() para comparar valores em vez de comparar os próprios valores. Agora seus itens estão distribuídos em cem buckets em vez de um único bucket. Quando você ver se existe um item, primeiro calcule o código de hash, vá para o intervalo correspondente e procure o item.

Essa técnica é usada pelas classes Hashtable e Dictionary no .NET Framework. Ambos usam o código de hash para armazenar e acessar itens. Hashtable é uma coleção não genérica; Dicionário é uma coleção genérica.Alguns princípios importantes podem ser deduzidos disso. 
•	O hash não pode ser revertido. Diferentemente da criptografia, o hash é um processo unidirecional que produz uma 'impressão digital' de tamanho fixo de dados
•	Itens iguais devem ter códigos de hash iguais. Significa que você pode verificar se dois itens são iguais, verificando seus códigos de hash. 
•	A implementação do GetHashCode deve retornar o mesmo valor todo momento. Não deve depender da alteração de valores, como a data ou hora atual.Toda vez que você gerar hash para dados específicos, será a mesma saída (formulário de hash).
•	Usado para armazenando e comparação de dados. É usado para verificar a integridade dos dados, a comparação de cadeias, a autenticidade dos dados e, o mais importante, para segurança, o armazenamento de senhas. Senhas são excelentes candidatos para hash

Essas propriedades são importantes ao analisar o hash em um contexto de segurança. Se você colocar um parágrafo de texto em hash e alterar apenas uma letra, o código de hash será alterado, portanto, o hash será usado para verificar a integridade de uma mensagem.

Por exemplo, digamos que Alice e Bob desejam enviar uma mensagem um para o outro. Alice cria um hash da mensagem e envia o hash e a mensagem para Bob. Bob cria um hash da mensagem que recebeu de Alice e compara os dois códigos de hash. Se eles corresponderem, Bob sabe que recebeu a mensagem correta.

Obviamente, sem nenhuma criptografia adicional, terceiros ainda podem adulterar a mensagem alterando a mensagem e o código de hash. Combinado com as tecnologias de criptografia oferecidas pelo .NET Framework, o hash é uma técnica importante para validar a autenticidade de uma mensagem.

Exemplo Armazenamento de senhas

Um outro exemplo poderia ser o armazenamento de senhas no banco de dados para que, se alguém roubasse o banco de dados, o hacker não decobriria a senha, pois estaria em formato ilegível. Você pode salvar uma senha com hash em um banco de dados ou comparar um usuário conectado, convertendo a senha em formato de hash e comparando seu hash com um valor de hash já armazenado desse usuário específico.

static void Main(string[] args)
{
    Console.WriteLine("Digite uma senha para o hash ...");
    // Obtem senha e armazena em pw
    string pw = Console.ReadLine();
    Console.WriteLine();

    // Cria valor hash da senha 
    string pwh = CreateHash_InBytes(pw);
    string pwh_64 = CreateHash_ToBase64(pw);

    // Exibir valor hash da senha 
    Console.WriteLine("The hash value for " + pw + " InBytes: " + pwh);
    Console.WriteLine("The hash value for " + pw + " ToBase64: " + pwh_64);

    //Poderíamos armazenar valor hash da senha em um banco de dados...
    //e usá-la novamente para autenticar o usuário, comparando-a com
    //a senha digitada quando o usuário tentar efetuar o login novamente
    Console.WriteLine();

    // Obtem a senha do usuário novamente
    Console.WriteLine("Digite a senha novamente para comparar os hashes ...");
    string pw2 = Console.ReadLine();

    // Hash a segunda senha e compare as duas
    string pwh2 = CreateHash_InBytes(pw2);
    Console.WriteLine();
    Console.WriteLine("Primeiro hash : " + pwh);
    Console.WriteLine("Segundo hash: " + pwh2);

    if (pwh == pwh2)
    {
        Console.WriteLine("Hashes iguais.");
    }
    else
    {
        Console.WriteLine("Hashes diferentes.");
    }

    Console.ReadKey();
}

public static string CreateHash_InBytes(string input)
{
    // senha em bytes
    var passwordInBytes = Encoding.UTF8.GetBytes(input);

    // Crie o objeto SHA256
    HashAlgorithm sha = SHA256.Create();

    byte[] hashInBytes = sha.ComputeHash(passwordInBytes);

    var hashedData = new StringBuilder();
    foreach (var item in hashInBytes)
    {
        hashedData.Append(item);
    }

    return hashedData.ToString();
}

public static string CreateHash_ToBase64(string input)
{
    HashAlgorithm sha = SHA256.Create();
    byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(input));
    return Convert.ToBase64String(hashData);
}

Há um problema nesse processo. Sempre que uma solicitação está sendo enviada ou um usuário faz login, o mesmo hash é gerado. Assim, o hacker pode rastrear o tráfego através de um canal de comunicação e saber que, toda vez que os dados são percorridos, o valor da senha/mensagem/hash é o mesmo. Portanto, o hacker pode enviar o mesmo valor sem saber o que é e pode entrar com êxito no seu sistema, o que é uma violação de segurança. Para evitar esse tipo de problema, temos o Salt Hashing.

Como os hashes são quebrados?

1.	Dicionário e ataques de força bruta
 
A maneira mais simples de decifrar um hash é tentar adivinhar a senha, fazer o hash de cada palpite e verificar se o hash do palpite é igual ao hash que está sendo quebrado. Se os hashes forem iguais, o palpite é a senha. As duas maneiras mais comuns de adivinhar senhas são ataques de dicionário e ataques de força bruta.

Um ataque de dicionário usa um arquivo que contém palavras, frases, senhas comuns e outras strings que provavelmente serão usadas como senha. Cada palavra no arquivo é hash e seu hash é comparado ao hash da senha. Se eles corresponderem, essa palavra é a senha. Esses arquivos de dicionário são construídos extraindo palavras de grandes corpos de texto e até de bancos de dados reais de senhas. O processamento adicional é frequentemente aplicado aos arquivos do dicionário, como substituir as palavras pelos equivalentes "leet speak" ("olá" torna-se "h3110"), para torná-los mais eficazes.

Um ataque de força bruta tenta todas as combinações possíveis de caracteres até um determinado comprimento. Esses ataques são muito onerosos em termos de computação e geralmente são os menos eficientes em termos de hashes quebrados por tempo de processador, mas sempre acabam encontrando a senha. As senhas devem ser longas o suficiente para que a pesquisa em todas as seqüências de caracteres possíveis para descobrir que demore muito a valer a pena.

Não há como impedir ataques de dicionário ou ataques de força bruta. Eles podem ser menos eficazes, mas não há como evitá-los completamente. Se o seu sistema de hash de senha for seguro, a única maneira de decifrar os hashes será executar um ataque de dicionário ou força bruta em cada hash.

2.	Tabelas de pesquisa
 
As tabelas de pesquisa são um método extremamente eficaz para quebrar rapidamente muitos hashes do mesmo tipo. A idéia geral é pré-calcular os hashes das senhas em um dicionário de senhas e armazená-las e a senha correspondente em uma estrutura de dados da tabela de pesquisa. Uma boa implementação de uma tabela de pesquisa pode processar centenas de pesquisas de hash por segundo, mesmo quando elas contêm muitos bilhões de hashes.

Se você quiser ter uma idéia melhor de como as tabelas de pesquisa podem ser rápidas, tente quebrar os seguintes hashes sha256 com o cracker gratuito de hash do CrackStation.

3.	Tabelas de pesquisa inversa
 

Esse ataque permite que um invasor aplique um ataque de dicionário ou força bruta a muitos hashes ao mesmo tempo, sem precisar pré-calcular uma tabela de pesquisa.

Primeiro, o invasor cria uma tabela de pesquisa que mapeia cada hash de senha do banco de dados de conta de usuário comprometido para uma lista de usuários que possuem esse hash. O invasor faz o hash de cada palpite de senha e usa a tabela de pesquisa para obter uma lista de usuários cuja senha era a palpite do invasor. Esse ataque é especialmente eficaz porque é comum que muitos usuários tenham a mesma senha.

4.	Rainbow Tables

As tabelas do arco-íris são uma técnica de troca da memória do tempo. Eles são como tabelas de pesquisa, exceto que eles sacrificam a velocidade de quebra de hash para tornar as tabelas de pesquisa menores. Por serem menores, as soluções para mais hashes podem ser armazenadas na mesma quantidade de espaço, tornando-as mais eficazes. Existem tabelas arco-íris que podem quebrar qualquer hash md5 de uma senha com até 8 caracteres.

Salt Hashing

Salt é um dado aleatório não repetitivo que é adicionado com o valor de hash para torná-lo único sempre que é gerado. Exemplo:
•	A palavra “Hello” será hash
•	O salt “xLhljFD" (Guid) é anexado a"Hello "
•	Quando for recuperar o valor de hash "Hello" posteriormente para comparar os valores, o sal "xLhljFD" deve ser anexado novamente para que os hashes correspondam

O Salt não precisa ser secreto. Apenas randomizando os hashes, as tabelas de pesquisa, as tabelas de pesquisa inversa e as tabelas arco-íris se tornam ineficazes. Um invasor não saberá com antecedência qual será o sal; portanto, não poderá calcular previamente uma tabela de pesquisa ou tabela de arco-íris. Se a senha de cada usuário tiver um hash diferente, o ataque à tabela de pesquisa inversa também não funcionará.

Para armazenar uma senha com Salt:
1.	Gere um longo sal aleatório usando um CSPRNG.
2.	Anexe o salt à senha e faça o hash com uma função de hash de senha padrão, como Argon2, bcrypt, scrypt ou PBKDF2.
3.	Salve o salt e o hash no registro do banco de dados do usuário.

Para validar uma senha com Salt:
1.	Recupere o sal e o hash do usuário do banco de dados.
2.	Anexe o salt à senha fornecida e faça o hash usando a mesma função de hash.
3.	Compare o hash da senha fornecida com o hash do banco de dados. Se eles corresponderem, a senha está correta. Caso contrário, a senha está incorreta.

Os erros mais comuns de implementação de Saltestão:

1.	Reutilização de Salt
Um erro comum é usar o mesmo Salt em cada hash. O Salt é codificado no programa ou é gerado aleatoriamente uma vez. Isso é ineficaz porque, se dois usuários tiverem a mesma senha, eles ainda terão o mesmo hash. Um invasor ainda pode usar um ataque de tabela de pesquisa inversa para executar um ataque de dicionário em cada hash ao mesmo tempo. Eles apenas precisam aplicar o Salt em cada palpite de senha antes de fazer o hash. Se o Salt for codificado em um produto popular, tabelas de pesquisa e tabelas arco-íris podem ser construídas para esse Salt, para facilitar o crack de hashes gerados pelo produto.

Um novo Salt aleatório deve ser gerado toda vez que um usuário cria uma conta ou altera sua senha.

2.	Salt Curto
Se o Salt for muito curto, um invasor poderá criar uma tabela de pesquisa para cada Salt possível. Por exemplo, se o Salt tiver apenas três caracteres ASCII, haverá apenas 95x95x95 = 857.375 Salts possíveis. Pode parecer muito, mas se cada tabela de pesquisa contiver apenas 1 MB das senhas mais comuns, coletivamente serão apenas 837 GB, o que não é muito, considerando que os discos rígidos de 1000 GB podem ser comprados por menos de US $ 100 hoje.

Pelo mesmo motivo, o nome de usuário não deve ser usado como Salt. Os nomes de usuário podem ser exclusivos para um único serviço, mas são previsíveis e geralmente reutilizados para contas em outros serviços. Um invasor pode criar tabelas de pesquisa para nomes de usuário comuns e usá-las para quebrar Salt hashes por nome de usuário.

Para tornar impossível para um invasor criar uma tabela de pesquisa para cada Salt possível, o Salt deve ser longo. Uma boa regra geral é usar um Salt que seja do mesmo tamanho que a saída da função hash. Por exemplo, a saída do SHA256 é de 256 bits (32 bytes), portanto, o salt deve ter pelo menos 32 bytes aleatórios.

public static void Create_SaltHash()
{
    // senha a ser hash
    string password = "HelloWorld";
    // gera Salt (GUID é um identificador uniqe globalmente)
    Guid salt = Guid.NewGuid();
    // Mescla senha com valor Salt
    string saltedPassword = password + salt;
    // senha em bytes
    var passwordInBytes = Encoding.UTF8.GetBytes(saltedPassword);
    // Crie o objeto SHA512
    HashAlgorithm sha512 = SHA512.Create();
    // gera o hash
    byte[] hashInBytes = sha512.ComputeHash(passwordInBytes);
    var hashedData = new StringBuilder();
    foreach (var item in hashInBytes)
    {
        hashedData.Append(item);
    }

    HashAlgorithm sha = SHA256.Create();
    byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(saltedPassword));
    string hashedData_64 = Convert.ToBase64String(hashData);

    Console.WriteLine("A senha Salt Hash InBytes é:" + hashedData.ToString());
    Console.WriteLine("A senha Salt Hash ToBase64 é:" + hashedData_64);
}

O método NewGuid criou um identificador exclusivo global, ou seja, alterou uma concatenação de valor com uma senha para gerar um hash diferente toda vez que o código é executado; portanto, o hash de sal protege você contra um ataque de segurança por hackers. 

Gerenciar e criar Certificados  Digitais

Embora a comunicação possa parecer segura às vezes, quando duas partes se comunicam, elas precisam ter certeza de que estão conversando com o parceiro certo. Por exemplo, quando você deseja fazer uma transação bancária via Internet, precisa verificar se está no site do banco e não em algum site que esteja falsificando a identidade do seu banco. Você também deseja saber que a comunicação está protegida. Para aplicativos da web, existem dois protocolos que resolvem esse problema: TLS (Transport Layer Security) e SSL (Secure Socket Layer). Ambos criptografam dados e garantem a autenticidade dos dados. Para autenticidade, eles usam PKI (Public Key Infrastructure) que é a infraestrutura necessária para lidar com certificados digitais. A PKI usa uma noção chamada Certificado Autoridade (CA). Uma CA é uma entidade que emite certificados digitais. Os certificados digitais vinculam uma chave pública a uma identidade. Ao fazer isso, quando duas partes desejam se comunicar e a parte que envia quer ter certeza de que conversa com a parte certa, a parte que envia pode usar a PKI para verificar a identidade da parte receptora.

O princípio é simples. Você pode optar por confiar diretamente na outra parte, mas isso pode se tornar complicado se você precisar fazer isso para todas as partes ou optar por confiar em terceiros, em vez de apenas verificar a identidade de entidades diferentes. Essa segunda opção é hierárquica, o que significa que o número de entidades em que você escolhe confiar é limitado. Essas entidades podem verificar a identidade de outras entidades, que se tornam entidades confiáveis e assim por diante, até que uma delas confie na entidade com a qual você deseja se comunicar e confirme sua identidade.

Os certificados de nível superior nos quais você escolhe confiar são chamados de certificados raiz. Normalmente você não adicione quaisquer certificados raiz. Eles vêm com a instalação do Windows ou via Atualizações do Windows. Se você deseja ver em quais certificados raiz o seu computador está configurado para confiar por padrão, vá ao Painel de Controle e abra a caixa de diálogo Opções da Internet. A partir daí, abra a guia Conteúdo e pressione o botão Certificados; escolha a guia Autoridades de certificação raiz confiáveis. Como você pode ver, a lista é longa, mas pode poupar muitos problemas se você deseja ter uma comunicação segura.
 

A diferença entre SSH e SSL

SSH e SSL são as duas formas de proteger as suas comunicações através da Internet. Ambos usam criptografia para impedir que outras pessoas em sua rede de ser capaz de ler as suas senhas ou informações bancárias . Internet Security. SSL e SSH são melhorias em relação aos métodos anteriores de comunicação na Internet. Métodos mais antigos, como Telnet e HTTP enviadas apenas informações sensíveis através da rede em forma de texto claro . Isso fez com senhas e outros dados vulneráveis a espionagem . É recomendável que você sempre use SSH e métodos de comunicação SSL para seus dados privados.
•	Secure Sockets Layer: SSL é uma forma de proteger a informação que você está visualizando com um browser. É a melhor maneira de acessar contas bancárias e enviar senhas para servidores Web na Internet. Você sabe que você está usando SSL se o endereço da Web que você está acessando é prefaciado com HTTPS ao invés de HTTP , que não está protegido.
•	Secure Shell: SSH é utilizado para enviar comandos de texto ou arquivos de forma segura em um servidor Web na Internet. Uma conexão SSH criptografa tudo o que você enviar através dele , de modo que suas informações não podem ser interceptados sem o seu conhecimento . Para tirar proveito do SSH você deve usar um cliente SSH , como o programa PuTTY livre para Windows

Protocolo de aperto de mão

Se um site agora deseja garantir sua identidade, deve verificar apenas sua identidade com uma das entidades nas quais você escolheu confiar. Um exemplo é a Microsoft. No seu computador, como você pode ver na figura anterior, existe uma Autoridade de Certificação Raiz. Se você apontar o navegador para https://www.microsoft.com (observe o https). 

Você pode visualizar rapidamente os detalhes do certificado do site que está visualizando no momento, na janela Informações da página do Firefox. Quando você navega em um site cujo endereço da Web começa com https, haverá um ícone de cadeado no início da barra de endereços. Faça o seguinte para visualizar um certificado:
1.	Clique no ícone do cadeado na barra de endereço
2.	Clique na seta à direita no painel suspenso Informações do site.
3.	No próximo painel, que mostrará quem verificou o certificado, clique no botão Mais informações.
 

4.	Na guia Segurança na janela Informações da página que é aberta, clique no botão Exibir certificado
 
5.	A guia Visualizador de certificados que abrirá exibirá informações detalhadas sobre o certificado, como emissor, período de validade, impressões digitais e muito mais.
 

Sites seguros têm um certificado SSL, o que significa que todos os dados transmitidos entre servidores e clientes da Web são criptografados. Se um site não for seguro, qualquer informação inserida pelo usuário na página (por exemplo, nome, endereço, detalhes do cartão de crédito) não será protegida e poderá ser roubada. No entanto, em um site seguro, o código é criptografado para que nenhuma informação sensível possa ser rastreada.
 

No protocolo de registro SSL, os dados do aplicativo são divididos em fragmentos. O fragmento é compactado e, em seguida, é anexado o MAC (Código de autenticação de mensagens) gerado por algoritmos como SHA (Secure Hash Protocol) e MD5 (Message Digest). Depois que a criptografia dos dados estiver concluída e o último cabeçalho SSL for anexado aos dados.
 

Protocolo de Handshake é usado para estabelecer sessões. Esse protocolo permite que o cliente e o servidor se autentiquem enviando uma série de mensagens um para o outro. O protocolo Handshake usa quatro fases para completar seu ciclo.
1.	o navegador do Cliente solicita primeiro que o servidor da Web se identifique. Isso solicita que o servidor da Web envie ao navegador uma cópia do certificado SSL. O Servidor envia pacotes de hello e nesta sessão de IP, o conjunto de criptografia e a versão do protocolo são trocados por motivos de segurança.
2.	O servidor responde ao navegador com uma confirmação assinada digitalmente para iniciar uma sessão criptografada SSL. 
3.	Nesta fase, o Cliente verifica se:
a)	O certificado enviado é confiável.
b)	O certificado é válido.
c)	O certificado está relacionado com o site que o enviou.
Uma vez que as informações acima tenham sido confirmadas, responde ao servidor enviando seu certificado e chave pública de troca do cliente e as mensagens podem ser trocadas.
4.	término deste protocolo de handshake. Isso permite que os dados criptografados sejam compartilhados entre o navegador e o servidor. Você pode perceber que sua sessão de navegação agora começa com https (e não http). Uma mensagem que tenha sido criptografada com uma chave pública poderá ser decifrada com a sua chave privada (simétrica) correspondente.

Se você tiver um certificado com a chave privada instalada localmente, poderá usá-lo para descriptografar os dados criptografados com a chave pública correspondente. Uma certificação digital usa hash e criptografia assimétrica para autenticar a identidade do proprietário (objeto assinado) para outras pessoas.Um certificado digital autentica a identidade de qualquer objeto assinado pelo certificado. Também ajuda a proteger a integridade dos dados.

Um proprietário do certificado contém chaves públicas e privadas. A chave pública é usada para criptografar a mensagem enviada enquanto a chave privada é usada para descriptografá-la; somente o proprietário do certificado tem acesso à chave privada para descriptografar a mensagem criptografada. Dessa forma, os certificados digitais permitem a integridade dos dados.

Se Alice envia uma mensagem para Bob, ela primeiro faz o hash da mensagem para gerar um código de hash. Alice então criptografa o código hash com sua chave privada para criar uma assinatura pessoal. Bob recebe a mensagem e a assinatura de Alice. Ele descriptografa a assinatura usando a chave pública de Alice e agora possui a mensagem e o código de hash. Ele pode então fazer o hash da mensagem e ver se o código de hash e o código de hash de Alice correspondem.

Um certificado digital faz parte de uma infraestrutura de chave pública (PKI, Public Key Infrastructure). Uma infraestrutura de chave pública é um sistema de certificados digitais, autoridades de certificação e outras autoridades de registro para verificar e autenticar a validade de cada parte envolvida.

Autoridade de Certificação (CA)

A Autoridade de Certificação (CA) é um emissor de certificados de terceiros que é considerado confiável por todas as partes. Cada certificado contém uma chave pública e os dados, como um sujeito para o qual o certificado é emitido, uma data de validade por quanto tempo o certificado permanecerá validado e as informações sobre o emissor que emitiu o certificado.

Usaremos uma ferramenta, Makecert.exe, que nos ajudará a criar um certificado digital X.509 O certificado X.509 é um padrão amplamente usado para definir certificados digitais, geralmente usado para autenticar clientes e servidores, criptografar e assinar digitalmente mensagens. Siga as etapas a seguir para criar um certificado digital:
1.	Execute o prompt de comando como administrador.
2.	Para criar um certificado digital, digite o seguinte comando: makecert {Certificate_Name}.cer (makecert myCert.cer)
 

 

O comando acima criará um arquivo de certificado com o nome "myCert.cer". Para usar o arquivo de certificado gerado, você deve instalá-lo em sua máquina para poder usá-lo.

Armazenamento de Certificados é um local onde você armazenou o certificado após a instalação. O Windows oferece dois locais de loja representados pela enumeração StoreLocation. Os valores possíveis são mostrados na tabela abaixo.
Membro	Descrição
CurrentUser	Representa o armazenamento de certificados usado pelo usuário atual
LocalMachine	Representa o armazenamento de certificados comum a todos os usuários na máquina local

O Windows oferece oito lojas predefinidas representadas pela enumeração StoreName. Os valores possíveis são mostrados na tabela abaixo.
Membro	Descrição
AddressBook	Armazenamento de certificados para outros usuários
AuthRoot	Armazenamento de certificados para CAs de terceiros
CertificateAuthority	Armazenamento de certificados para CAs intermediárias
Disallowed	Armazenamento de certificados para certificados revogados
My	Armazenamento  de certificados para certificados pessoais
Root	Armazenamento de certificados para CAs raiz confiáveis
TrustedPeople	Armazenamento de certificados para pessoas e recursos diretamente confiáveis
TrustedPublisher	Armazenamento de certificados para editores diretamente confiáveis

Siga as etapas a seguir para criar e instalar um certificado.
1.	Execute o prompt de comando como administrador.
2.	makecert -n "CN = myCert" -sr currentuser -ss myCertStore
O comando acima criará e instalará um arquivo de certificado.

 

O padrão usado pela PKI é X.509, que especifica o formato das PKIs, a Lista de Revogação do Certificado (CLR), atributos para os certificados e como validar o caminho do certificado. O .NET Framework implementa o padrão X.509 e todas as classes necessárias para criar e gerenciar os certificados são definidas no espaço para nome System.Security.Cryptography.X509Certificates.

O programa a seguir inicia abrindo no modo somente leitura o armazenamento raiz da máquina local e, em seguida, mostra como você pode usar o X509Store para imprimir o nome e a data de validade de todos os certificados existentes.

X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
store.Open(OpenFlags.ReadOnly);
Console.WriteLine("Friendly Name\t\t\t\t\t Expiration date");
foreach (X509Certificate2 certificate in store.Certificates)
{
    Console.WriteLine("{0}\t{1}", certificate.FriendlyName, certificate.NotAfter);
}
store.Close();

Para trabalhar com certificados programaticamente, você pode usar a classe X509Certificate2. Os certificados usam a noção de repositórios de certificados, que são os locais onde os certificados são mantidos com segurança. No .NET, os repositórios são implementados na classe X509Store. O programa abaixo mostra como usar este certificado gerado para assinar e verificar algum texto. Os dados são hash e depois assinados. Ao verificar, o mesmo algoritmo de hash é usado para garantir que os dados não foram alterados.

static void Main(string[] args)
{
    string textToSign = "Test paragraph";
    byte[] signature = Sign(textToSign);

    var hashedData = new StringBuilder();
    foreach (var item in signature)
    {
        hashedData.Append(item);
    }

    Console.WriteLine(textToSign + " em hash = " + hashedData.ToString());

    // Uncomment this line to make the verification step fail
    // signature[0] = 0;
    var verifica = Verify(textToSign, signature);

    Console.WriteLine(verifica);
    Console.ReadKey();
}

public static byte[] Sign(string text)
{
    X509Certificate2 cert = GetCertificate();
    var csp = (RSACryptoServiceProvider)cert.PrivateKey;
    byte[] hash = HashData(text);
    return csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
}

static bool Verify(string text, byte[] signature)
{
    X509Certificate2 cert = GetCertificate();
    var csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
    byte[] hash = HashData(text);
    return csp.VerifyHash(hash,
    CryptoConfig.MapNameToOID("SHA1"),
    signature);
}

private static byte[] HashData(string text)
{
    HashAlgorithm hashAlgorithm = new SHA1Managed();
    UnicodeEncoding encoding = new UnicodeEncoding();
    byte[] data = encoding.GetBytes(text);
    byte[] hash = hashAlgorithm.ComputeHash(data);
    return hash;
}

private static X509Certificate2 GetCertificate()
{
    X509Store my = new X509Store("myCertStore", StoreLocation.CurrentUser);
    my.Open(OpenFlags.ReadOnly);
    var certificate = my.Certificates[0];
    return certificate;
}

O método SignHash usa a chave privada do certificado para criar uma assinatura para os dados. O VerifyHash usa a chave pública do certificado para verificar se os dados foram alterados.

Um uso de certificados digitais é proteger a comunicação na Internet. O popular protocolo de comunicação HTTPS é usado para proteger a comunicação entre um servidor web e um cliente. Certificados digitais são usados para garantir que o cliente esteja conversando com o servidor da Web correto, não com um impostor.

CAS (CODE ACCESS SECURITY)

O namespace System.Security contém os blocos de construção fundamentais de uma estrutura de segurança de acesso ao código .NET. O namespace filho System.Security.Permissions fornece CAS (Code Access Security), que é uma tecnologia de segurança desenvolvida para fornecer a capacidade de proteger os recursos do sistema quando um assembly .NET é executado. Esses recursos do sistema podem ser: arquivos locais, arquivos em um sistema de arquivos remoto, chaves do registro, bancos de dados, impressoras e assim por diante. O acesso irrestrito a esses tipos de recursos pode levar a riscos de segurança em potencial, pois o código malicioso pode executar operações prejudiciais, como remover arquivos críticos, modificar chaves do Registro ou excluir dados armazenados nos bancos de dados, sugerindo alguns.

Em vez de dar confiança total a todos os aplicativos, oCAS fornece a capacidade do .Net de limitar os privilégios dos aplicativos, além do tradicional Role Based Security (RBS).O CAS não substitui o RBS, apenas o complementa. Ao contrário do RBS, o CAS não possui usuários.O CAS executa as seguintes funções no .NET Framework:
•	Define permissões para acessar recursos do sistema.
•	Permite que o código exija que seus chamadores tenham permissões específicas. Por exemplo, uma biblioteca que expõe métodos que criam arquivos deve impor que seus chamadores tenham o direito de E/S de arquivo.
•	Permite que o código exija que seus chamadores possuam uma assinatura digital. Dessa forma, o código pode garantir que seja chamado apenas pelos chamadores de uma organização ou local específico.
•	Força todas essas restrições em tempo de execução. Um código em execução pode ser negado acesso a um recurso
•	Um meio de atribuir identidade ou evidência a .Net assemblies. As identidades do usuário não estão nesta imagem; só identidades de montagem
•	Adequado para ambientes onde código parcialmente confiável é executado
•	O .Net Framework usa o CAS para fornecer uma sandbox opcional para código gerenciado.

O CAS antes do .NET Framework 4.0

A transparência nível 1 foi introduzida na versão 2.0 do .NET Framework e foi usada principalmente apenas dentro da Microsoft. No entanto, a transparência nível 1 foi mantida para que você possa identificar código legado que deve ser executado com as regras de segurança anteriores. Anteriormente ao .NET framework 4.0, os problemas com CAS eram:
•	Aplicar o CAS não nunca foi uma tarefa fácil. Muitas etapas enigmáticas da criação de grupos de códigos e conjuntos de permissões, etc. Devido a essa dificuldade de implementação haviam duas ferramentas que auxiliavam na configuração do CAS:
o	.NET Configuration Tool, Mas, de acordo com o MSDN, essa ferramenta foi removida no .NET 4.0.
o	Code Access Security Policy Tool (caspol.exe) pelo prompt de comando. No .NET Framework versão 3.5 e versões anteriores, a diretiva CAS está sempre em vigor. No .NET Framework 4, a política CAS deve estar habilitada. Sugeriu fazer a seguinte entrada no arquivo app.config ou Machine.config para suprimir a mensagem de aviso.
<configuração>
<runtime>
<LegacyCasPolicy enabled = "true" />
</runtime>
</configuration>
•	Se você precisasse mover a montagem para um computador diferente, precisaria fazer o trabalho inteiro novamente.
•	O CAS não funciona com código não gerenciado. O CAS pode ser aplicado apenas em aplicativos gerenciados. Os aplicativos não gerenciados são executados sem nenhuma restrição do CAS e são limitados apenas pela segurança baseada em funções do sistema operacional. 

Aqui está uma breve descrição de como, antes do .NET Framework 4.0, o Code Access Security permitia que desenvolvedores e administradores de sistemas protegessem recursos definindo:
•	Autenticação: lida com evidências de montagem(e não os principals do Windows). Em tempo de carregamento. o CLR recolhe provas {evidence) sobre o assembly. Esta evidência fica associada ao Assembly. 
•	Autorização: não relacionado ao objetos de nível SO, em vez disso, eles se preocupam com a aplicação padrão tarefas como acessar um banco de dados por meio de um Provedor OLEDB ou um recurso na Internet através de um URL. Ainda em tempo de carregamento, o CLR usa informacao de configuração {Policy Levels) para transformar a Evidence de um assembly em um  conjunto de permissões. A relação entre evidências e conjuntos de permissões podem ser administradas por quatro políticas diferentes, que correspondem à função que o usuário representa:
o	Administrador de Domínio - Política Corporativa (Enterprise). Política para uma família de máquinas que fazem parte de um Active Directory
o	Administrador de Máquinas - Política de Máquina (Machine)
o	Usuário real da máquina - Política do Usuário (User)
o	Desenvolvedor - Política do Aplicativo (App Domain)
As três primeiras políticas são armazenados em XML arquivos e são administrados através do NET ferramenta de configuração 1.1 (Mscorcfg.msc). A política final é administrado através de código para o domínio de aplicativo atual.   Essas políticas são configuráveis após a implantação do aplicativo e podem ser modificadas a qualquer momento.
•	Permissão: possibllidade de realizar uma ação. Permissões CAS estão diretamente anexadas amontagens em tempo de carregamento. As permissões são explicitamente exigidas pelos objetos (tipicamente em bibliotecas seguras). Este conjunto de permissões fica associado a todos os tipos do assembly, em tempo de execução, os métodos submetido verificam se os chamadores possuem as permissões necessárias.
Processo de determinação das permissões dum assembly {permission grant)
o	Entrada: Evidence
o	Saida: PermlssionSet
o	Parametrização: PolicyLevel
 
•	Uma propriedade de montagem chamada Evidence, que atua como uma espécie de combinação da identidade da montagem em relação à zona de onde veio, a identidade do editor da montagem, a identidade da própria montagem ou simplesmente sua localização. Evidência se refere à identidade do aplicativo e não depende do utilizador
Existem 7 tipos de evidência padrão, que podem ser divididos em dois grupos:
1)	Evidência do assembly - responde à pergunta "Quem é o autor do assembly" Por exemplo, todas as classes Microsoft CLR assinadas com o mesmo par de chaves pública/privada, o que permite ao CLR determinar que os desenvolvedores da Microsoft escreveram o código e concedem confiança total (controle total ) para o sistema.
	Diretório do aplicativo: local de execução da aplicação.
	Hash: valor de hash do assembly
	Editor(Publisher): informações sobre o criador do assembly, no caso deste conter um certificado.
	Nome forte (Strong Name): nome do assembly.
2)	Evidência de host - responde às perguntas "De onde veio o assembly?" Se você iniciou um smart client fazendo referência à localização da URL e, posteriormente, mova o executável para um disco rígido local, o CLR não rastreia o histórico de sua localização.
	Local: o nome do host da URL / Domínio Remoto / VPN.
	URL: o URL completo da qual o código se originou.
	Zona: classificação da localização original do assembly (intranet, Internet, MyComputer, Trusted, Untrusted, NoZone).
 
•	Um conjunto de grupos de códigos (Code Groups), que contém todos os assemblies que possuem uma específica Evidence. Cada Code Group tem um PermissionSet específico atribuído.
•	Um conjunto (PermissionSets) de permissões (Permissions) que um assembly ou método deve ter para acessar recursos críticos (de uma perspectiva de segurança). 

Quando um assembly é carregado, o CLR do .NET Framework verifica suas Evidence e atribui a ele apenas as permissões específicas permitidas para seu grupo de códigos. Se o assembly tiver todas as permissões concedidas, é considerado totalmente confiável, caso contrário, é parcialmente confiável. 

Enquanto o Evidence é atribuído toda vez que um assembly é executado em tempo de execução(CLR), os Grupos de Códigos e o PermissionSet relacionado são armazenados dentro da máquina e podem ser modificados ou criados novos pelos administradores do sistema. 

Os desenvolvedores podem interagir com a permissão atribuída à sua montagem de uma das duas maneiras:
1.	Declarativo; usando um conjunto de atributos que podem ser atribuídos a um assembly ou a suas classes e/ou métodos (propriedades incluídas nos acessadores).
•	Garantir que o CLRnunca execute nosso aplicativo sem que as permissões de segurança necessárias. 
•	Restringir ainda mais nosso código para que, mesmo que o assembly seja hackeado, ele não levará a um comprometimento maior de todo o sistema. 
•	Garantir que nosso aplicativo possa ser executado com permissões CAS limitadas e, como resultado, seja capaz de executar em zona parcialmente confiável.
•	Existem exigências que só podem ser expressas na forma declarativa
•	Facilita a análise estática
•	Podem ser aplicadas a todo um tipo
•	São realizadas no início do método

[assembly:FileIOPermissionAttribute(SecurityAction.RequestMinimum, Read=@"C:\bootfile.ini")]
namespace MyDeclarativeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static void DeclarativeCAS()
        {
            try
            {
                System.IO.File.Delete("COLORFUL.txt");
                Console.WriteLine("DeclarativeCAS");
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }
        }
    }
}

2.	Imperativo; usando um conjunto de classes dentro dos métodos de uma montagem, solicitamos explicitamente a permissão no código.
•	Possibilita lógica mais complexa na determinação da permissão a exigir. Ex.: exigência dependente do valor dos parâmetros

public static void ImperativeCAS()
{
    PermissionSet perms = new PermissionSet(PermissionState.None);
    perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, @"C:\Windows"));
    perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Inetpub"));
    perms.AddPermission(new RegistryPermission(RegistryPermissionAccess.Write, @"HKEY_LOCAL_MACHINE\Software"));

    FileIOPermission f = new FileIOPermission(PermissionState.None);
    f.AllLocalFiles = FileIOPermissionAccess.Read;
    try
    {
        Console.WriteLine("ImperativeCAS");
        f.Demand();
        perms.Demand();
    }
    catch (SecurityException s)
    {
        Console.WriteLine(s.Message);
    }
}

Interface IPermission

A classe base para todos os itens relacionados ao CAS é System.Security.CodeAccessPermission. Permissões herdadas do CodeAccessPermission são permissões como FileIOPermission, ReflectionPermission ou SecurityPermission. Ao aplicar uma dessas permissões, você solicita ao CLR a permissão para executar uma operação protegida ou acessar um recurso. Porém nem todas as permissõesderivam de CodeAccessPermission (o que implicam percurso no stack) como, por exemplo, a permissão PrincipalPermission que verifica a identidade e os roles do utilizador currente.

Para identificar manualmente os requisitos de permissão, você precisa analisar seu código e determinar os tipos de recursos que ele acessa, o tipo de acesso que requer (como leitura/gravação) e as operações privilegiadas que ele executa.A principal dificuldade em tentar identificar manualmente os requisitos de permissão ocorre se o seu código chamar outros assemblies, como assemblies de terceiros ou assemblies de sistema. Identificar seus requisitos de permissão pode ser muito difícil. 
Cada permissão de acesso ao código representa um dos seguintes direitos:
•	O direito de acessar um recurso protegido, como um arquivo
•	O direito de executar uma operação protegida, como acessar código não gerenciado

Por exemplo, para controlar o acesso ao sistema de arquivos, FileIOPermissionAttribute (declarativamente) ou uma instância da classe FileIOPermission (imperativamente) pode ser usada. 
Classe	Direito de acesso
AspNetHostingPermission	Recursos em ambientes hospedados no ASP.NET
DataProtectionPermission	Dados criptografados
DirectoryServicesPermission	System.DirectoryServices
DnsPermission	Sistema de Nomes de Domínio
EnvironmentPermission	Variáveis de ambiente
EventLogPermission	Log de eventos
FileDialogPermission	Arquivos Selecionados
FileIOPermission	Arquivos ou diretórios
GacIdentityPermission	Cache de montagem(assembly) global
IsolatedStorageFilePermission	Armazenamento isolado
IUnrestrictedPermission	Interface
KeyContainerPermission	Contêineres de criptografia de chave pública
MessageQueuePermission	Filas de mensagens
OdbcPermission	ODBC
OleDbPermission	OLE DB
OraclePermission	Banco de dados Oracle
PerformanceCounterPermission	Perf. contadores
PrincipalPermission	Controle de acesso
PrintingPermission	Impressoras
ReflectionPermission	Descubra informações sobre um tipo
RegistryPermission	Chaves e valores do registro
SecurityPermission	Código não gerenciado
ServiceControllerPermission	Serviços
SiteIdentityPermission	Permissão de identidade
SocketPermission	Faça ou aceite conexões
SqlClientPermission	Bancos de dados SQL
StorePermission	Sores contendo certificados X.509
StrongNameIdentityPermission	Permissão para nomes fortes
UIPermission	Funcionalidade da interface do usuário
UrlIdentityPermission	Permissão de identidade para o URL
WebPermission	Conexões em um endereço da Web
ZoneIdentityPermission	Zona da qual o código se origina

Para verificar as permissões atribuídas ao seu código, os desenvolvedores podem chamar métodos como Demand(), LinkDemand() ou InheritanceDemand()ou substituí-los usando métodos como Assert(), Deny() ou PermitOnly(). As classes de atributo de permissão definem a propriedade Action são:
SecurityAction	Descrição	Fase	Alvos	Imperativa
LinkDemand	Exige que o chamador tenha a permissão	Compilação JIT	Classe, método	Não
InheritanceDemand	Exige que classes derivadas tenham a permissão	Carregamento	Classe, método	Não
Demand	Verifica se os chamadores tern permissão para realizar a operação	Na Execução, alteram o comportamento do percurso numa stack frame
	Classe, método	Sim
Assert	Garante a existence da permissao. lermmando o percurso no stack		Classe, método	Sim
Deny	Recusa a permissão. terminando o percurso no stack		Classe, método	Sim
PermitOnly	Apenas permile esta permissão		Classe, método	Sim
RequestMinimum	Conjunto mínimo de permissões para o assembly se poder executar	Resolvido em tempo de determinação de permissões do assembly	Assembly	Não
RequestOptional	Conjunto opcional de permissões que o assembly pode ter		Assembly	Não
RequestRefuse	Conjunto permissões que o assembly não pode ter		Assembly	Não

PermissionSet

A classe PermissionSet representa um conjunto de permissões
•	Implementa a interface IStackWalk - utilizada para exigir permissões e/ou alterar os percursos no stack
•	Implementa semântica de conjunto
o	IPermission Copy()
o	IPermission intersect(IPermission)
o	IPermission Unlon(IPermission)
o	Bool IsSubsetOf(IPermission)

Abaixo um exemplo com operações de conjuntos:

public static void Escreva_Permissoes()
{
    IPermission perm1 = new FileIOPermission(FileIOPermissionAccess.Read, @"C:\Windows");
    IPermission perm2 = new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Windows");
    IPermission perm3 = new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Windows");
    IPermission perm4 = new FileIOPermission(FileIOPermissionAccess.AllAccess, @"C:\Windows");
    IPermission all = new FileIOPermission(PermissionState.Unrestricted);
    IPermission none = new FileIOPermission(PermissionState.None);

    Console.WriteLine(perm1.Union(perm2)); // IPermission Read="C:\Windows" e Write="C:\Windows"
    Console.WriteLine(perm2.Union(perm3)); // IPermission Write="C:\Windows"
    Console.WriteLine(perm1.Intersect(perm4)); // IPermission Read="C:\Windows" 
    Console.WriteLine(perm1.Union(all)); // IPermission Unrestricted="true"
    Console.WriteLine(perm1.Intersect(all)); // IPermission Read="C:\Windows"
    Console.WriteLine(perm1.Union(none)); // IPermission Read="C:\Windows"

    PermissionSet perms = new PermissionSet(PermissionState.None);
    perms.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
    perms.AddPermission(new UIPermission(UIPermissionWindow.AllWindows));
    perms.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));

    Console.WriteLine(perms);
    // PermissionSet 
    // IPermission FileDialogPermission Access="Open"
    // IPermission SecurityPermission Flags="Execution"
    // IPermission UIPermission Window="AllWindows"

    perms.AddPermission(perm1);
    perms.AddPermission(perm1.Union(perm2));
    Console.WriteLine(perms);
    // Adicionou IPermission Read="C:\Windows" e Write="C:\Windows"
}

Interface IStacKWalk

Um conceito importante do CAS é que todos os elementos da pilha de chamadas atual sejam verificados. A pilha de chamadas é uma estrutura de dados que armazena informações sobre todos os métodos ativos em um momento específico. Portanto, se o seu aplicativo iniciar no método Main e chamar o método A, que chama o método B, todos os três métodos estarão na pilha de chamadas. Quando o método B retorna, apenas Main e A estão na pilha de chamadas.

O CAS percorre a pilha de chamadas e verifica se todos os elementos da pilha têm as permissões necessárias. Dessa forma, você pode ter certeza de que um método menos confiável não pode chamar algum código restrito por meio de um método altamente confiável.

A exigência de algumas permissões implica verificar se todos os chamadores no call stack possuem a permissão, o Common Language Runtime (CLR) realiza isso por meio de um Stack-Walk, que pode ser comparado ao seguinte cenário:

"O adolescente Joe de 16 anos não podia ir legalmente à loja para comprar cerveja, então pede ao amigo Bill de17 anos para que ele convença o pai a comprar a cerveja.O pai de Bill sabe que ele está infringindo a lei, mas ele ainda o faz. O balconista verifica a carteira de motorista do pai de Bill e, como ele tem mais de 18 anos, vende um pacote de cerveja para ele". 

A cadeia de pedidos de Joe -> Bill -> pai de Bill para o funcionário representa o conceito de software do Stack-Walk.
  

	No mundo real, a cerveja será vendida porque há apenas uma verificação de identificação do pai de Bill. Joe e Bill representam código parcialmente confiável e o pai de Bill representa código totalmente confiável no que diz respeito ao sistema. É exatamente isso que acontece quando os vírus obtêm acesso a um recurso seguro. No cenário do .NET Framework, o CLR impede que um Stack-Walk bem-sucedido aconteça, porque solicita o ID adequado em todos os níveis da célula da cadeia; portanto, se alguém da cadeia não tiver um ID adequado, consequentemente, ele rejeitará as solicitações em todos os níveis. Portanto, se as regras de reforço da CLR fossem aplicadas a um cenário da vida real, Joe ou Bill nunca teriam a cerveja. O cenário a seguir falhará quando atingir Bill. Porém, às vezes, parte do código que é parcialmente confiável (não tem acesso total ao sistema) precisa ter acesso a recursos totalmente confiáveis e é aí que os modificadores no stack-walk entram em cena.

No diagrama acima, o Balconista nega (Deny) qualquer pessoa que não tenha ID (Evidência) adequada para vender cerveja, mas o pai de Bill permite (PermitOnly) que seu filho e o amigo do filho bebam cerveja porque ele quase atingiu a idade legal. Quando Bill pega sua cerveja, ele a compartilha com seu amigo (Assert) e eles apreciam cerveja juntos em um dia ensolarado. O Code Access Security fornece essa flexibilidade, mas precisamos estar cientes de que também apresenta um maior risco à segurança.

Para restringir o acesso de uma extensão no momento do desenvolvimento, você pode utilizar o arquivo AssemblyInfo.cs localizado dentro de Properties do projeto e definir atributos no nível de transparência do assembly:
 

Ou atribuir declarativamente no assembly (dll ou exe):
 

 

As regras a seguir se aplicam ao uso de atributos no nível do assembly:
Atributo	Descrição
Nenhum atributo	Se você não especificar nenhum atributo, o CLR interpretará todo o código como segurança crítica, exceto onde a segurança crítica viola uma regra de herança (por exemplo, ao substituir ou implementar um método de interface ou virtual transparente ). Nesses casos, os métodos são de segurança crítica. Especificar nenhum atributo faz com que o Common Language Runtime determine as regras de transparência para você.
SecurityTransparent	Todo o código é transparente; o assembly inteiro não fará nada privilegiado nem seguro.
SecurityCritical
	Todo o código introduzido por tipos neste assembly é crítico; todo o outro código é transparente. Esse cenário é semelhante a não especificar nenhum atributo; no entanto, o Common Language Runtime não determina automaticamente as regras de transparência. Por exemplo, se você substituir um método virtual ou abstract ou implementar um método de interface, por padrão, esse método será transparente. Você deve anotar explicitamente o método como SecurityCritical ou SecuritySafeCritical; caso contrário, uma TypeLoadException será lançada no tempo de carregamento. Essa regra também se aplica quando a classe base e a classe derivada estão no mesmo assembly.

CAS no .NET Framework 4.0

O modelo Code Access Security foi completamente redesenhado no .NET Framework 4.0, a ponto de as políticas CAS terem sido completamente removidas e agora tudo funciona através da transparência de segurança Level 2, que foi projetado para eliminar a necessidade do modelo de Diretiva CAS, usado até o .NET Framework 3.5. 

As alterações no .NET 4.0 são amplamente reativas, pois abordam os problemas existentes, em vez de implementar novos recursos. Ao longo dos anos, o modelo CAS, implementado na versão pré-4.0 do .NET Framework, revelou alguns problemas que não são tão triviais para resolver. Em particular:
•	Todo o trabalho que deve ser feito para configurar uma Política CAS bem-sucedida, ou seja, todo o trabalho necessário para definir o PermissionSet correto e os Grupos de Código deveria ser feita para cada máquina específica. Isso desencorajou muitos administradores de implementar a tecnologia em seus sistemas.
•	Quando um aplicativo específico precisava ser movido para um sistema diferente, a política de segurança diferente aplicada a esse novo sistema poderia causar problemas no próprio aplicativo. Por exemplo, se um arquivo executável pudesse ser executado corretamente na máquina do desenvolvedor, às vezes, quando precisava ser movido para um servidor de produção ou um compartilhamento remoto, o mesmo executável poderia parar de funcionar repentinamente.
•	Ao desenvolver código, não era tão fácil configurar os recursos do CAS para o assembly. Isso ocorria porque os administradores precisavam configurar políticas CAS muito diferentes em suas máquinas, e o desenvolvedor tinha que ter em mente todos os locais possíveis em que seus assemblies poderiam ser executados, bem como quais eram os possíveis PermissionSets. Os desenvolvedores não tinham como conhecer as decisões dos administradores com antecedência.
•	As políticas do CAS eram muito úteis quando os administradores precisavam controlar o que o software podia ou não fazer, mas as políticas do CAS não tinham nenhum efeito sobre o código não gerenciado.

Portanto, a equipe de segurança do Microsoft .NET decidiu reconstruir o Code Access Security a partir do zero. As principais diferenças podem ser resumidas em:
1)	Todo o sistema de políticas do CAS foi completamente removido. As decisões sobre quais permissões podem ser concedidas a um assembly agora são tomadas pelo host no qual o assembly é executado. Isso elimina todos os problemas relacionados à configuração de políticas do CAS.
2)	O mecanismo de imposição, ou seja, o mecanismo usado noCLR para forçar um assembly a executar apenas o código que tem permissão para executar, foi substituído pelo modelo Transparente de Segurança. Isso simplifica muito do trabalho necessário para definir as condições de acesso aos recursos que a montagem deve usar.

Transparente de Segurança Level2

O modelo Transparente de Segurança foi introduzido no .NET Framework 2.0, mas, até a versão 4.0, ele só podia ser usado no nível do assembly e era usado principalmente para impedir que o código transparente de segurança elevasse privilégios. De fato, o código transparente de segurança não podia nem usar o método Assert. Nas versões anteriores à 4.0 do .NET Framework, a transparência não podia ser usada para imposição, pois a imposição era tratada pelo sistema de política da CAS; esse comportamento agora é chamado de Level1 Security Transparency. Com o .NET Framework 4.0, essas limitações foram removidas e o modelo Transparente de Segurança se tornou a maneira padrão de proteger recursos. O novo modelo foi chamado de Level2 Security Transparency e agora veremos como ele funciona.

O modelo Level2 Security Transparency divide todo o código em três categorias: código SecurityCritical, código SecurityTransparent e código SecuritySafeCritical. Vamos ver em detalhes o que eles podem ou não fazer:
Categorias	Descrição
SecurityTransparent	possui privilégios limitados nos recursos do sistema e não tem acesso ao código SecurityCritical. Além disso, ele não pode chamar código nativo nem elevar permissões. Além disso, métodos transparentes não podem herdar tipo críticos, substituir métodos virtuais críticos ou implementar métodos de interface críticos.
SecuritySafeCritical	é totalmente confiável, mas pode ser chamado pelo código transparente. Ele expõe uma área de superfície limitada do código de confiança total; as verificações de correção e segurança acontecem no código de segurança crítica.
O código SecuritySafeCritical fornece uma espécie de ponte entre o código SecurityTransparent e o código SecurityCritical. De fato, o código SecurityTransparent pode chamar o código SecuritySafeCritical, que por sua vez pode chamar o código SecurityCritical. O código SecuritySafeCritical é considerado totalmente confiável e tem a mesma permissão do código SecurityCritical.
SecurityCritical	pode chamar qualquer código e é totalmente confiável, mas não pode ser chamado por código transparente.Esse código pode ser chamado por outro código SecurityCritical ou pelo código SecuritySafeCritical, mas não pode ser chamado pelo código SecurityTransparent.
Atributo
AllowPartiallyTrustedCallers 	Todos os padrões de código são transparentes. No entanto, tipos individuais e membros podem ter outros atributos.
À partir do .Net Framework 4.0, a Microsoft modificou um componente interno do chamado CAS (Code Access Security), que é quem determina como as classes podem acessar umas às outras. Essa modificação faz com que algumas aplicações parem de funcionar exibindo a seguinte mensagem de erro: “System.Security.SecurityException: That assembly does not allow partially trusted callers”. Obrigando a colocar esse atributo no assemblyconfig.
 

Observe que, devido ao fato de o código SecurityTransparent não poder chamar o código SecurityCritical, a Level2 Security Transparency se tornou um mecanismo de imposição.

Vamos começar com alguns exemplos para demonstrar o modelo. Digamos que começamos a escrever um aplicativo de console simples que nos ajuda a explorar as configurações de segurança de um assembly que escrevemos. Para fazer isso, usamos as seguintes novas propriedades do .NET Framework 4.0:
Propriedade	Descrição
Assembly.SecurityRuleSet	Indica qual regra de segurança é usada em nossa montagem: transparência de segurança nível 1 ou transparência de segurança nível 2.
Assembly.IsFullTrusted	Se verdadeiro, o assembly está sendo executado como um assembly totalmente confiável e todos os seus métodos são SecurityCritical. Se falso, o assembly é parcialmente confiável e todos os seus métodos são SecurityTransparent.
Tipo.IsSecurityCritical	Se verdadeiro, o objeto está sendo executado como código SecurityCritical.
Type.IsSecuritySafeCritical	Se verdadeiro, o objeto está sendo executado como código SecuritySafeCritical.
Type.IsSecurityTransparent	Se verdadeiro, o objeto está sendo executado como código SecurityTransparent.

Inicialmente, escrevemos uma biblioteca CAS_01.dll simples que contém a seguinte classe:

public class AssemblyInfo 
{
    /// Write to the console the security settings of the assembly
    public string GetCasSecurityAttributes()
    {
        //gets the reference to the current assembly
        Assembly a = Assembly.GetExecutingAssembly();

        StringBuilder sb = new StringBuilder();

        //show the transparence level
        sb.AppendFormat("Security Rule Set: {0} \n\n", a.SecurityRuleSet);

        //show if it is full trusted
        sb.AppendFormat("Is Fully Trusted: {0} \n\n", a.IsFullyTrusted);

        //get the type for the main class of the assembly
        Type t = a.GetType("CAS_01.AssemblyInfo");

        //show if the class is Critical,Transparent or SafeCritical
        sb.AppendFormat("Class IsSecurityCritical: {0} \n", t.IsSecurityCritical);
        sb.AppendFormat("Class IsSecuritySafeCritical: {0} \n", t.IsSecuritySafeCritical);
        sb.AppendFormat("Class IsSecurityTransparent: {0} \n", t.IsSecurityTransparent);

        try
        {
            sb.AppendFormat("\nPermissions Count: {0} \n", a.PermissionSet.Count);
        }
        catch (Exception ex)
        {
            sb.AppendFormat("\nError while trying to get the Permission Count: {0} \n", ex.Message);
        }

        return sb.ToString();
    }
}

O método GetCasSecurityAttributes() dentro da classe retorna uma sequência que contém:
•	O conjunto de regras para a montagem (transparência de segurança nível 1 ou nível 2)
•	Se a montagem é totalmente confiável
•	Se a classe AssemblyInfo é SecurityCritical, SecurityTransaprent ou SecuritySafeCritical
•	O número de permissão no PermissionSet do assembly.

Em seguida, criamos um aplicativo de console que consome a classe AssemblyInfo exposta pela biblioteca anterior:

using CAS_01;
using System;
using System.Reflection;
using System.Security.Policy;

class Program
{
    static void Main(string[] args)
    {
 //get the assembly zone evidence
        Zone z = Assembly.GetExecutingAssembly().Evidence.GetHostEvidence<Zone>();
        Console.WriteLine("Zone Evidence: " + z.SecurityZone.ToString() + "\n");
        Console.WriteLine(new AssemblyInfo().GetCasSecurityAttributes());

        Console.ReadKey();
    }
}

O método principal grava o valor da evidência de zona do assembly no console e chama o método GetCasSecurityAttributes() da classe AssemblyInfo contida na dll. Quando executamos nosso programa, a saída que obtemos é:
 

Como podemos ver na Figura:
1)	A evidência para a montagem afirma que está sendo executada na máquina local.
2)	A transparência de segurança é fornecida como Transparência de segurança de nível 2. Não o definimos em nosso código, para que possamos ver que o Nível2 é o modo de Transparência de segurança padrão no .NET Framework 4.0. 

Se quisermos usar o modelo anterior do Nível 1, podemos usar o atributo da montagem:

[assembly: SecurityRules(SecurityRuleSet.Level1)]

Note que, ao fazer isso, a montagem se torna transparente, mas nosso programa ainda será executado. Isso ocorre porque a transparência de segurança de nível 1 não pode executar a imposição em nosso código porque, como mencionei anteriormente, a imposição com as versões anteriores ao 4.0 do .NET Framework foi tratada pela política do CAS. Em breve veremos que essa falta de imposição não ocorre com a transparência de segurança de nível 2 e, se você deseja manter a compatibilidade com as versões anteriores ao 4.0 do CAS, deve ativar a política do CAS adicionando as seguintes linhas ao arquivo de configuração do assembly.

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <runtime>
    <NetFx40_LegacySecurityPolicy enabled="true" />
  </runtime>
</configuration>

A montagem é totalmente confiável (fully trusted). Isso significa que todas as classes dentro dele são SecurityCritical e que não há permissões definidas para o assembly. Isso ocorre porque os assemblies executados em um computador ou em uma pasta compartilhada são considerados aplicativos não hospedados. Para explicar o que isso realmente significa, lembre-se de que vimos anteriormente que, com a transparência de segurança de nível 2, as permissões para uma montagem agora são decididas pelo host da montagem e não pelas políticas de segurança do CAS. Exemplos de host incluem CLRdo ASP.NET, SQL CRL e assim por diante. Os aplicativos executados fora desse host são chamados de aplicativos não hospedados e sempre são totalmente confiáveis por padrão.

Essa parece ser uma decisão natural que a Equipe de Segurança do .NET Framework tomou. Se todas as permissões estiverem definidas em um host, isso significa que aplicativos não hospedados, para os quais a permissão não pode ser definida, podem ser totalmente confiáveis ou absolutamente não confiáveis (ou seja, sem permissão para acessar recursos protegidos). No primeiro caso, os recursos ainda podem ser protegidos usando outras técnicas ou ferramentas, e veremos alguns deles no próximo parágrafo. No segundo caso, para permitir que o .NET Framework continue trabalhando usando recursos protegidos, seriam necessárias técnicas ou ferramentas capazes de elevar permissões. Do ponto de vista da segurança, essa segunda opção claramente não seria uma boa escolha.

Outra vantagem de os aplicativos não hospedados serem totalmente confiáveis é que, dessa maneira, eles podem ser executados como código SecurityCritical e não podem ser acessados pelo código SecurityTransparent. Portanto, o código que pertence à zona da Internet (por exemplo, SecurityTransparent) não pode usar aplicativos não hospedados para danificar nossos sistemas. Como já sabemos, o código SecurityTransparent não pode acessar o código SecurityCritical e, portanto, nosso aplicativo não hospedado é protegido contra código proveniente da Internet ou suspeito.

Vamos supor compilassemos dois projetos diferentes mas trocassemos apenas o nome da .dll e mantivessemos o mesmo namespace. 
 
 
Daria o seguinte erro:
 

Agora, suponha que gostaríamos de executar nosso .exe a partir de uma pasta compartilhada na rede. A saída que obteríamos seria:
 

Podemos ver na Figura que as evidências da zona de montagem foram alteradas para “Internet”, mas nossa montagem é mais uma vez totalmente confiável e as classes dentro dela são SecurityCritical; nenhuma permissão é aplicada ao assembly.

Com as versões anteriores à 4.0 do .NET Framework, as diferentes evidências de zona implicariam que um grupo de códigos diferente seria aplicado ao assembly e, em algumas situações, o mesmo código inexplicavelmente deixaria de funcionar. Se, por exemplo, o exe precisar acessar algum arquivo, poderá fazê-lo na máquina local (zona MyComputer), mas, quando movido para uma pasta compartilhada (zona Internet), lançaria uma exceção de segurança, interrompendo a execução. Pode parecer que exista segurança mínima inicial nesse novo sistema, mas vamos dar uma olhada em como podemos manter nosso código sob controle mais rígido.

Reduzindo permissões no modelo Level2 Security Transparency 

No exemplo anterior, a primeira coisa que pode ser lembrada é que, com a nova transparência de segurança de nível 2, a segurança geral dos sistemas parece estar diminuída. Ao ignorar o princípio do menor privilégio, o novo modelo certamente resultará em mais código do que antes, com total confiança para executar? Este não é realmente o caso; o modelo geral de segurança mudou apenas, tornando-se mais fácil de implementar e, assim, reduzindo a chance de erros potencialmente perigosos. Daqui a pouco, veremos como podemos reduzir as permissões de código em execução no modelo de nível 2. No entanto, se os administradores quiserem controlar qual tipo de código pode ser executado em um sistema específico, eles poderão usar ferramentas como Diretivas de Restrição de Software ou o novo AppLocker disponível no Windows 7 e Windows 2008 Server R2. Usando essas ferramentas mais recentes, eles podem controlar não apenas o código gerenciado (como a Política CAS herdada ativada), mas também o código não gerenciado.

Do ponto de vista do desenvolvedor, quando se trata de reduzir permissões, agora é possível executar o aplicativo como código SecurityTransparent se o aplicativo não precisar acessar recursos protegidos, é uma boa maneira de satisfazer as diretrizes do princípio do menor privilégio. Para forçar a montagem a ser executada como SecurityTransparent, basta inserir o seguinte atributo para a montagem:

[assembly: SecurityTransparent()]
namespace CAS_01
{

Isso indica que todo o código, mesmo que o assembly seja totalmente confiável, será do tipo SecurityTransparent. Se adicionarmos esta linha anterior ao nosso assembly de demonstração, obteremos a seguinte saída:
 

Como podemos ver, nossa montagem agora é SecurityTransparent, apesar de ser totalmente confiável, devido ao fato de também não ser hospedada. Como resultado, ele pode chamar apenas o código SecurityTransparent e, portanto, não pode ser usado para acessar recursos protegidos (SecurityCritical). De fato, como vemos, quando ele tenta obter a Contagem de Permissões, ocorre uma exceção, porque o código contido no acessador get da propriedade PermissionSet é o código SecurityCritical. O mesmo também acontece se o .exe for executado a partir de uma pasta compartilhada da rede.

Os exemplos fornecidos nesta seção parecem indicar que a transparência de segurança de nível 2 é, de fato, um modelo de tudo ou nada. Se o assembly é totalmente confiável, ele pode fazer qualquer coisa e, se o definirmos como SecurityTransparent, ele não poderá usar recursos protegidos. No entanto, uma abordagem mais granular é possível quando precisamos proteger recursos específicos, e é baseada no APTCA (Allow Parcialmente Confiável), que podemos definir para um assembly. Com ele, podemos definir o código como SecuritySafeCritical, criando assim uma ponte entre o código SecurityTransparent e SecurityCritical. 

Sandboxing

E se tivermos que usar um assembly de terceiros em que não confiamos totalmente, se o rodarmos em nossas máquinas e um atributo SecurityTransparent não foi especificado dentro do assembly, ele poderá fazer qualquer coisa com nossos recursos.

A solução é aplicar o sandbox na montagem, o que restringe os recursos que a montagem pode usar, permitindo a proteção de nossos sistemas. O sandboxing consiste na criação de um host parcialmente confiável e forçar a montagem a ser executada dentro dele. Como mencionei no início deste artigo, a transparência de segurança de nível 2 substituiu a política do CAS, deixando o host com a capacidade de definir permissões. Portanto, este é um método bastante elegante de criar uma sandbox, como veremos em breve. O host parcialmente confiável é criado com o método AppDomain.CreateDomain() definido no .NET Framework 4.0; Este método não é novo na versão 4.0, apenas foi modificado para permitir o sandboxing.

No domínio do aplicativo anterior à 4.0, as permissões para acessar recursos eram determinadas pela Política do CAS, que, para cada assembly carregado no domínio, aplicava restrições a ele com base em seu Grupo de Código, que por sua vez era determinado por suas Evidências, e as PermissionSet imposto ao próprio Grupo de Código. Isso leva a um domínio heterogêneo, no qual o PermissionSets pode misturar as configurações um do outro, provocando situações muito complexas. Com a transparência de segurança de nível 2, as permissões são impostas diretamente ao domínio e todos os assemblies dentro dele são forçados a segui-las (exceções são feitas para aquelas que o desenvolvedor decide que podem ser totalmente confiáveis). Isso foi chamado de domínio homogêneo.

Vamos continuar trabalhando em nossa biblioteca de CAS_01.dll e tentar executá-la em um domínio em área restrita (ou seja, parcialmente confiável). Para fazer isso, precisamos modificar a classe AssemblyInfo, permitindo derivar de MarshalByRefObject.

Public class AssemblyInfo: MarshalByRefObject

O Remote Object é implementando em uma classe que deriva de System.MarshalByRefObject.Portanto o Remotable Object é qualquer objeto fora do domínio da aplicação. Qualquer objeto pode ser transformado em um objeto remoto a partir da classe MarshalByRefObject.
Os 3 componentes principais do .NET Framework Remoting são:
1.	Remotable Object
2.	Remote Listener Application - (escuta requisições para o Remote Object)
3.	Remote Client Application - (efetua requisições para o Remote Object)

Você tem duas categorias de objetos em aplicativos distribuídos: 
•	Remotable: podem ser acessados fora de seu próprio domínio de aplicativo. O sistema de comunicação remota pode usar métodos e propriedades desse tipo de objeto.
•	Nonremotable: não pode ser acessado fora de seu próprio domínio de aplicativo. Esse tipo de objeto não permite que seus métodos ou propriedades sejam usados pelo sistema de comunicação remota. Objetos removíveis 

Existem dois tipos de objetos remotáveis
•	Marshal por valor: Quando o cliente chama um método em marechal-por-valor-objeto, o sistema de comunicação remota cria uma cópia desse objeto e passa a cópia para o domínio do aplicativo cliente. A cópia recebida pode lidar com qualquer chamada de método no domínio do cliente. O uso do Marshal por objeto de valor reduz a viagem que consome recursos pela rede.
•	Marshal por referência: Quando o cliente chama um método no Marshal por objeto de referência, o sistema de comunicação remota cria um objeto proxy no aplicativo de chamada que contém a referência de todos os métodos e propriedades do objeto.

Um rápido exemplo de aplicação utilizando MarshalByRefObject é dada abaixo:

//namespace MarshalPrintDomain
//{
public class Worker : MarshalByRefObject
{
    public void PrintDomain()
    {
        Console.WriteLine("Object is executing in AppDomain \"{0}\"",
            AppDomain.CurrentDomain.FriendlyName);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an ordinary instance in the current AppDomain
        Worker localWorker = new Worker();
        localWorker.PrintDomain();

        // Create a new application domain, create an instance of Worker in the 
        // application domain, and execute code there.
        // Nota: O exemplo e Worker não podem estar no mesmo namespace
        // Se sim, o código a seguir lançará uma exceção:
        // Não foi possível carregar o tipo 'Worker' do assembly 
        // Por isso foi comentado o namespace da aplicação
        AppDomain ad = AppDomain.CreateDomain("ByRef domain");
        Worker remoteWorker = (Worker)ad.CreateInstanceAndUnwrap(
        typeof(Worker).Assembly.FullName,
        typeof(Worker).Name);
        remoteWorker.PrintDomain();

        Console.ReadKey();
    }
}
//} 
 

Juntamente com o MarshalByRefObject, precisamos usar o método AppDomain.CreateDomain() quando queremos implementar o Sandboxing. Antes de podermos usar esse método, precisamos criar um PermissionSet que gostaríamos de ser concedido ao domínio recém-criado e, portanto, com o comportamento do Domínio Homogêneo do modelo Level2 Security Transparent, para os assemblies carregados nele.

Em vez de especificar toda a permissão uma por uma que gostaríamos de inserir no PermissionSet, podemos usar o novo método 4.0 Framework SecurityManager.GetStandardSandbox(), que permite retornar o PermissionSet associado com as evidências passadas como entrada. O código a seguir mostra como fazer isso:

/// create a permission set
public static PermissionSet GetPermissionSet()
{
    //create an evidence of type zone
    Evidence ev = new Evidence();
    ev.AddHostEvidence(new Zone(SecurityZone.MyComputer));

    //return the PermissionSets specific to the type of zone
    return SecurityManager.GetStandardSandbox(ev);
}

/// Get the Domain security info
public static string GetDomainInfo(AppDomain domain)
{
    StringBuilder sb = new StringBuilder();
    //check the domain trust
    sb.AppendFormat("Domain Is Full Trusted: {0} \n", domain.IsFullyTrusted);
    //show the number of the permission granted to the assembly
    sb.AppendFormat("\nPermissions Count: {0} \n", domain.PermissionSet.Count);
    return sb.ToString();
}

A linha de código acima retorna um objeto PermissionSet que contém todas as permissões associadas à evidência de zona do MyComputer. Em seguida, criamos um método que navega pelos recursos de segurança do domínio que criaremos:

public static void TesteSecurityTransparent()
{
    //create  the AppDomainSetup
    AppDomainSetup info = new AppDomainSetup();
    //set the path to the assembly to load. 
    info.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    //create the domain
    AppDomain domain = AppDomain.CreateDomain(
        "CasHostDemo", null, info, GetPermissionSet());
    //create an instance of the AssemblyInfo class
    Type t = typeof(AssemblyInfo);
    ObjectHandle handle = Activator.CreateInstanceFrom(
        domain,
        t.Assembly.ManifestModule.FullyQualifiedName,
        t.FullName);
    AssemblyInfo ai = (AssemblyInfo)handle.Unwrap();

    Console.WriteLine("DOMAIN INFO:\n");
    //get the domain info
    Console.WriteLine(GetDomainInfo(domain));

    Console.WriteLine("ASSEMBLY INFO:\n");
    //get the assembly info form the sandboxed assembly
    Console.WriteLine(ai.GetCasSecurityAttributes());
}

Apenas para explicar o que está acontecendo, o método principal:
1)	Cria um objeto AssemblyDomainSetup e define seu valor ApplicationBase para o diretório que contém nosso assembly de demonstração.
2)	Cria o domínio…
•	nomeando-o "CasHostDemo",
•	sem passar um objeto de evidência
•	usando o objeto AssemblyDomainSetup criado na etapa anterior e
•	definindo o PermissionSet obtido com o método GetPermissionSet () que desenvolvemos.
3)	Usa a classe Activator para criar um ObjectHandler que mantém a referência a um objeto do tipo AssemblyInfo (definido em nossa dll demo), em seguida, desembrulha-o em um objeto AssemblyInfo.
4)	Chama nosso método GetDomainInfo () passando para o domínio que criamos.
5)	Chama o método GetCasSecurityAttributes () do objeto AssemblyInfo instanciado.

Observe que não passamos nenhum objeto de evidência para o método AppDomain.CreateDomain, porque ele não precisa mais deles. Como a evidência não é mais usada para atribuir o grupo de códigos correto ao domínio usando as políticas da CAS, a evidência simplesmente não é mais necessária.Quando executamos nosso programa agora, obtemos a seguinte saída:
 

Como podemos ver, o uso da zona MyComputer como evidência não afeta nosso código. De fato, essa zona cria um domínio de confiança total sem permissões do PermissionSet. No entanto, se alterarmos a Zona MyComputer para, por exemplo, a Zona Internet, obteremos:
 

O domínio agora é executado como domínio parcialmente confiável e há 7 permissões concedidas (as mesmas 7 permissões relacionadas à Zona da Internet), o que significa que nosso assembly é executado agora como um assembly parcialmente confiável. Todas as classes são SecurityTransparent e o acessador da propriedade PermissionSet do assembly lança a mesma exceção vista anteriormente. O mesmo erro poderia ser obtido se declarassemos o assembly parcialmente confiável:

[assembly: AllowPartiallyTrustedCallers()]
namespace CAS_01
{
    Public class AssemblyInfo : MarshalByRefObject
    {
 

O motivo dessa exceção, conforme nos detalhes do Exception, o assembly 'CAS_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' está marcado com AllowPartiallyTrustedCallersAttribute e usa o modelo de transparência de segurança de nível 2. A transparência de nível 2 faz com que todos os métodos em assemblies AllowPartiallyTrustedCallers se tornem transparentes por padrão.

Caso comentássemos a herança MarshalByRefObject atribuída a AssemblyInfo:

namespace CAS_01
{
    Public class AssemblyInfo//: MarshalByRefObject
    {

	Teríamos o seguinte erro no momento de criar a instância do AssemblyInfo.

AssemblyInfo ai = (AssemblyInfo)handle.Unwrap();

System.Runtime.Serialization.SerializationException: O tipo 'CAS_01.AssemblyInfo' no assembly 'CAS_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' não está marcado como serializável.
 

O domínio em área restrita permite também a possibilidade de executar assemblies como totalmente confiáveis, mesmo que o domínio seja apenas parcialmente confiável: os assemblies contidos no Global Assembly Cache (GAC) são executados no modo totalmente confiável por padrão  Se quisermos adicionar um assembly que não seja do GAC à lista de assemblies totalmente confiáveis, basta informar o método Assembly.CreateDomain() listando esse assembly que não é do GAC usando seu StrongName. Obviamente, os conjuntos devem ser assinados com um arquivo de chave de nome forte. 

Criar chaves via sn.exe

A Microsoft disponibilizou um utilitário de linha de comando que permite-nos gerar uma strong name para assinarmos os Assemblys que desejarmos. Esse utilitário chama-se sn.exe (sn = strong name) e, para gerar um par de chave pública/privada, basta simplesmente fazer:
1)	Encontrar, prompt de comando do Visual Studio no StartMenu, execute-o como administrador.
2)	Navege para a pasta raiz do projeto da biblioteca de classes, cd “{PathLocation}”, pressione enter.  Se quiser, poderá abrir o Visual Studio 2005 Command Prompt para poupar a digitação do caminho completo até o utilitário.
3)	crie uma chave de nome forte escrevendo um commando “sn -k {KeyName}.snk”. O parâmetro –k indica ao utilitário para gerar a chave. Por exemplo, “sn -k myClassLibrarykey.snk” e pressione Enter.
 

Agora, para o arquivo snk, siga as seguintes etapas.
1)	Abra o arquivo AssemblyInfo.cs do projeto, (este arquivo está abaixo do arquivo Properties do Solution Explorer.)
2)	Associe uma chave de nome forte à montagem adicionando um atributo de montagem e o local de uma chave de nome forte, como [assembly: AssemblyKeyFile ("myClassLibrarykey.snk")]
3)	Pressione ctrl + shift + B para complilar a solução. Isso associará o par de chaves de nome forte à montagem. Lembre-se, o Visual Studio deve estar executando como administrador.
 

Por fim, modificamos o método anterior da seguinte maneira:

public static void TesteNoGAC()
{
    //create the AppDomainSetup
    AppDomainSetup info = new AppDomainSetup();
    //set the path to the assembly to load. 
    info.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    Assembly a = Assembly.LoadFile(Path.Combine(info.ApplicationBase, "CAS_01.dll"));
    StrongName sName = a.Evidence.GetHostEvidence<StrongName>();
    //StrongName fullTrustAssembly = typeof(AssemblyInfo).Assembly.Evidence.GetHostEvidence<StrongName>();
    //create the domain
    AppDomain domain = AppDomain.CreateDomain(
        "CasHostDemo", null, info, GetPermissionSet(), new StrongName[] { sName });

    //create an instance of the AseemblyInfo class
    Type t = typeof(AssemblyInfo);
    ObjectHandle handle = Activator.CreateInstanceFrom(
        domain,
        t.Assembly.ManifestModule.FullyQualifiedName,
        t.FullName);
    AssemblyInfo ai = (AssemblyInfo)handle.Unwrap();

    Console.WriteLine("DOMAIN INFO:\n");
    //get the domain info
    Console.WriteLine(GetDomainInfo(domain));

    Console.WriteLine("ASSEMBLY INFO:\n");
    //get the assembly info form the sandboxed assembly
    Console.WriteLine(ai.GetCasSecurityAttributes());
}

Neste novo método principal, carregamos o assembly de demonstração do arquivo System e obtemos seu StrongName:

Assembly a = Assembly.LoadFile(Path.Combine(info.ApplicationBase, "CAS_01.dll"));
StrongName sName = a.Evidence.GetHostEvidence<StrongName>();

Em seguida, usamos uma sobrecarga diferente do método AppDomain.CreateDomain(), que permite definir quais assemblies devem ser considerados confiança total e passamos a ele o StrongName do assembly de demonstração.

AppDomain domain = AppDomain.CreateDomain("CasHostDemo", null, info, GetPermissionSet(), new StrongName[] { sName });

Ao executar nosso .exe, obtemos:
 

Podemos ver que, enquanto o domínio permanece parcialmente confiável, o assembly é executado no modo de confiança total e todas as classes nele são SecurityCritical.

Pelo que vimos até agora, parece que o novo modelo Level2 SecurityTransparence é uma tecnologia tudo ou nada, ou seja, se a montagem for totalmente confiável, todos os recursos estarão disponíveis e, se for apenas parcialmente confiável, nenhum deles será.

Felizmente, este não é o caso, como veremos neste artigo. Ao proteger recursos, a fim de permitir uma abordagem mais granular à segurança, um assembly pode ser marcado com o atributo Permitir Montagem de Chamadas Parcialmente Confiáveis (APTCA). Dessa maneira, os atributos de segurança ficam disponíveis no nível da classe ou do método, resultando em configurações mais flexíveis.

O atributo AllowPartiallyTrustedCallers (APTCA)

O atributo Permitir chamadas parcialmente confiáveis (APCTA) é um atributo com escopo de assembly que altera a maneira como o assembly responde ao modelo de transparência de segurança de nível 2. Quando usadas, as seguintes modificações ocorrem:

Todas as classes e métodos dentro do assembly se tornaram SecurityTransparent, a menos que especificado de outra forma.Para especificar um comportamento diferente, os atributos SecurityCritical ou SecuritySafeCritical podem ser adicionados às implementações de classe e/ou método desejadas.

O atributo APCTA é muito semelhante ao atributo SecurityTransparent, que usamos para forçar um assembly a ser executado como SecurityTransparent. Como resultado, quando o assembly do chamador tentou acessar o código SecurityCritical, uma exceção foi lançada (lembra-se da propriedade PermissionSet?) Como mencionado, as principais diferenças entre os dois atributos estão no fato de que, quando o atributo APCTA substitui o atributo SecurityTransparent, podemos especificar diretamente as configurações de segurança para cada classe ou método em uma montagem através do uso dos atributos SecurityCritical e/ou SecuritySafeCritical. Se o assembly fosse marcado como SecurityTransparent, esses dois atributos não teriam efeito, devido ao fato de o atributo SecurityTransparent funcionar apenas no nível do assembly e não ser inferior.

Portanto, com o atributo APCTA, somos capazes de:
1)	Elevar as permissões de uma classe ou método individual, transformando-o em uma classe ou método SecuritySafeCritical. Ao fazer isso, concedemos à classe ou método todas as permissões para acessar recursos protegidos (como código SecurityCritical) enquanto ele permanece visível para o código SecurityTransparent. Essencialmente, criamos uma espécie de ponte entre o código SecurityTransparent e SecurityCritical.
2)	Manter algumas classes ou métodos protegidos do assembly parcialmente confiável, marcando-os como SecurityCritical.

Como veremos em breve, esses dois recursos removem o suposto comportamento "tudo ou nada" do atributo SecurityTransparent. Para provar isso, começaremos reutilizando o exemplo fornecido no artigo anterior, com algumas modificações:

public class AssemblyInfo 
{
    /// <summary>
    /// Write to the console the security settings of the assembly
    /// </summary>
    public string GetCasSecurityAttributes()
    {
        //gets the reference to the current assembly
        Assembly a = Assembly.GetExecutingAssembly();

        StringBuilder sb = new StringBuilder();

        //show the transparence level
        sb.AppendFormat("Security Rule Set: {0} \n\n", a.SecurityRuleSet);

        //show if it is full trusted
        sb.AppendFormat("Is Fully Trusted: {0} \n\n", a.IsFullyTrusted);

        //get the type for the main class of the assembly
        Type t = a.GetType("CAS_01.AssemblyInfo");

        //show if the class is Critical,Transparent or SafeCritical
        sb.AppendFormat("Class IsSecurityCritical: {0} \n", t.IsSecurityCritical);
        sb.AppendFormat("Class IsSecuritySafeCritical: {0} \n", t.IsSecuritySafeCritical);
        sb.AppendFormat("Class IsSecurityTransparent: {0} \n", t.IsSecurityTransparent);

        try
        {
            sb.AppendFormat("\nPermissions Count: {0} \n", a.PermissionSet.Count);
        }
        catch (Exception ex)
        {
            sb.AppendFormat("\nError while trying to get the Permission Count: {0} \n", ex.Message);
        }

        return sb.ToString();

    }
}


Com relação à versão anterior desta biblioteca DLL, inserimos o seguinte código antes da declaração do espaço para nome:

[assembly: AllowPartiallyTrustedCallers()]
[assembly: SecurityRules(SecurityRuleSet.Level2)]
namespace CAS_02
{

Que afirma que nossa assembléia agora é uma assembléia da APCTA. Também adicionamos as seguintes linhas de código:

//get the MethodInfo object of the current method              
MethodInfo m = t.GetMethod("GetCasSecurityAttributes");
//show if the current method is Critical, Transparent or SafeCritical
sb.AppendFormat("Method IsSecurityCritical: {0} \n", m.IsSecurityCritical);
sb.AppendFormat("Method IsSecuritySafeCritical: {0} \n", m.IsSecuritySafeCritical);
sb.AppendFormat("Method IsSecurityTransparent: {0} \n", m.IsSecurityTransparent);

Que permitem ver se o método GetCasSecurityAttributes é SecurityCritical, SecuritySafeCritical ou SecurityTransparent. Ao executar o aplicativo de console que usamos para consumir o assembly anterior, obtemos a seguinte saída:
 

Observando a figura, podemos ver rapidamente que:
•	O assembly está sendo executado no computador local,
•	A montagem é totalmente confiável, mas a classe AssemblyInfo é transparente e…
•	Até o método GetCasSecurityAttributes é transparente;
•	Ao tentar obter o valor PermissionSet.Count, obtemos uma exceção que nos lembra que o assembly está marcado com o atributo APTCA; portanto, todas as suas classes e métodos são SecurityTransparen e não podem chamar o código SecurityCritical.

Neste ponto, parece que estamos observando o mesmo comportamento que teríamos obtido usando o atributo de assembly SecurityTransparent. A diferença está no fato de que o atributo APTCA nos permite definir o nível de segurança do código de maneira mais granular. Com ele, podemos modificar diretamente o nível de segurança do método GetCasSecurityAttributes, tornando-o SecurityCritical ou SecuritySafeCritical. Nesse ponto, optaremos por defini-lo como SecurityCritical:

[SecurityCritical()]
Public string GetCasSecurityAttributes()
{

E executando o .exe pela segunda vez, obtemos o seguinte resultado:
 

Como você pode ver, a mensagem de exceção desapareceu porque, mesmo que a classe seja SecurityTransparent, o método subjacente agora é SecurityCritical e pode executar o acessador da propriedade PermissionSet. Apenas para demonstrar a diferença entre os atributos APTCA e SecurityTransparent, se substituirmos a seguinte linha:

[assembly: AllowPartiallyTrustedCallers()]
[assembly: SecurityRules(SecurityRuleSet.Level2)]
namespace CAS_02
{

Com:

[assembly: SecurityTransparent()]
[assembly: SecurityRules(SecurityRuleSet.Level2)]
namespace CAS_02
{

Que usamos na parte I desta curta série (como mencionei no início desta seção), obtemos uma saída familiar:

 

Como esperado, o atributo SecurityCritical no GetCasSecurityAttributes agora não tem efeito e o método permanece SecurityTransparent.

Recursos Personalizados

Apesar da simplicidade do exemplo anterior, os atributos SecurityCritical e SecuritySafeCritical podem ser combinados nos assemblies APCTA de maneiras muito diferentes para configurar estratégias de proteção personalizadas. Em vez de sempre invocar os mesmos recursos clássicos protegidos de um sistema, vamos ver um exemplo que mostra como o modelo de transparência de segurança de nível 2 pode ser usado para proteger qualquer tipo de recurso que desejamos, indo além do modelo de política CAS herdado. Considere a seguinte classe CasWriter, definida dentro de um assembly de demonstração chamado CasWriterDemo.dll:

[assembly: AllowPartiallyTrustedCallers()]
namespace CasWriterDemo
{
    public class CasWriter
    {

        /// Write a sentence to console
        [SecurityCritical()]
        public virtual void WriteCustomSentence(string text)
        {
            Console.WriteLine(text + "\n");
        }

        /// Write a sentence to console
        [SecuritySafeCritical()]
        public void WriteDefaultSentence(int index)
        {
            switch (index)
            {
                case 0:
                    WriteCustomSentence("homo homini lupus");
                    break;
                case 1:
                    WriteCustomSentence("melius abundare quam deficere");
                    break;
                case 2:
                    WriteCustomSentence("audaces fortuna iuvat");
                    break;
            }
        }

        /// Get the Security status of each method developed
        public string GetMethodsSecurityStatus()
        {
            //get the MethodInfo of each method
            MethodInfo[] infos = GetType().GetMethods();
            StringBuilder sb = new StringBuilder();
            foreach (MethodInfo m in infos)
            {
                if (m.ReturnType != typeof(void)) continue;
                sb.Append("\n");
                sb.Append(m.Name + ": ");
                if (m.IsSecurityCritical)
                {
                    sb.AppendFormat("Method IsSecurityCritical: {0} \n", m.IsSecurityCritical);
                }
                else if (m.IsSecuritySafeCritical)
                {
                    sb.AppendFormat("Method IsSecuritySafeCritical: {0} \n", m.IsSecuritySafeCritical);
                }
                else if (m.IsSecurityTransparent)
                {
                    sb.AppendFormat("Method IsSecurityTransparent: {0} \n", m.IsSecurityTransparent);
                }
            }
            return sb.ToString();
        }
    }
}

A classe possui os seguintes três métodos estáticos:
•	WriteCustomSentence (texto da string): esse método grava uma frase, passada para ela como entrada, no console.
•	WriteDefaultSentence (int index): esse método grava uma sentença fixa no console, selecionando entre três valores possíveis. O parâmetro de entrada indica qual sentença escrever.
•	string GetMethodsSecurityStatus (): esse método retorna, como uma string, o status de segurança dos dois métodos anteriores.

Agora, escrevemos um aplicativo de console (CasWriterTeste.exe) que consome os métodos anteriores:

[assembly: SecurityTransparent()]
namespace CasWriterTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            CasWriter writer = new CasWriterInheritance();
            Console.WriteLine(writer.GetMethodsSecurityStatus());


            //CasWriter writer = new CasWriter();
            //Console.WriteLine(writer.GetMethodsSecurityStatus());
            try
            {
                Console.Write("Custom Sentence: ");
                writer.WriteCustomSentence("Barba non facit philosophum");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n" + ex.Message + "\n\n");
            }
            try
            {
                Console.Write("Default Sentence: ");
                writer.WriteDefaultSentence(new Random().Next(0, 2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n" + ex.Message);
            }
            Console.ReadKey();
        }
    }
}

Marcamos o assembly CasWriterTeste.exe como SecurityTransparent porque queremos testar o que acontece quando o assembly CasWriterDemo.dll é chamado por código parcialmente confiável.

Dado que o CasWriterDemo.dll está marcado com o atributo APTCA, todo o código dentro dele é SecurityTransparent e, portanto, devemos esperar que o aplicativo seja executado corretamente. Estamos em uma situação em que o código SecurityTransparent chama outro código SecurityTransparent e o modelo Level2 SecurityTransparent certamente permite isso. Executando o aplicativo, obtemos o seguinte resultado:
 

Vimos na figura acima que, como esperado, os dois métodos são ambos SecurityTransparent e as frases foram gravadas corretamente no console. Agora, suponha que desejamos impedir que um código parcialmente confiável seja capaz de escrever uma frase personalizada e deixe-o apenas com a capacidade de escrever uma frase padrão selecionada em um índice. Nessa situação, o WriteCustomSentence torna-se nosso recurso protegido. Para conseguir isso, precisamos:
1)	Marque o método WriteCustomSentence como SecurityCritical, para que o código SecurityTransparent não possa acessá-lo.
2)	Marque o método WriteDefaultSentence como SecuritySafeCritical.

Essa segunda modificação deve parecer um pouco estranha; afinal, o método WriteDefaultSentence já é SecurityTransparent e, portanto, pode ser acessado por outro código SecurityTransparent. Nosso executável também é SecurityTransparent, portanto, ele também pode acessar o método SecurityTransparent WriteDefaultSentence. No entanto, observe que o método WriteDefaultSentence usa o método WriteCustomSentence após a seleção de uma frase.

O efeito geral é que o método SecurityTransparent WriteDefaultSentence agora chama um método SecurityCritical: WriteCustomSentence. Portanto, se tentarmos chamar WriteDefaultSentence a partir do código SecurityTransparent, obteremos uma exceção; vamos tentar executar nosso .exe sem a segunda modificação:
 

Como podemos ver, o método WriteCustomSentence agora é SecurityCritical e não pode ser acessado pelo código SecurityTransparent. Você pode encontrar a exceção associada a esse comportamento após a linha “Custom personalizada:” na figura. Para recapitular rapidamente, o método WriteDefaultSentence é SecurityTransparent, portanto, o método principal do .exe pode acessá-lo, mas quando o WriteDefaultSentence tenta usar o WriteCustomSentence para gravar a saída no console, ocorre uma exceção, como você pode ver após a linha “Sentença Padrão:” na figura.

Portanto, analisando cada etapa envolvida nesta demonstração, temos:
a)	O método Main chama WriteCustomSentence, o que leva a uma exceção. Um método SecurityTransparent não pode chamar um método SecurityCritical.
b)	O método Main chama WriteDefaultSentence, que é bem-sucedido. Um método SecurityTransparent pode chamar um método SecurityTransparent.
c)	O método WriteDefaultSentence chama WriteCustomSentence, o que leva a uma exceção. Um método SecurityTransparent não pode chamar um método SecurityCritical.

Se, conforme sugerido na segunda modificação acima, marcarmos o método WriteDefaultSentence como SecuritySafeCritical, resolveremos esse possível problema. O código SecuritySafeCritical foi projetado para atuar como uma ponte de permissão, na medida em que pode ser chamado pelo código SecurityTransparent e, por sua vez, pode chamar o código SecurityCritical. Portanto, com essa modificação, criaremos uma ponte entre o código SecurityTransparent (o método Main) e o código SecurityCritical (o método WriteCustomSentence). Se agora executarmos o nosso .exe, veremos este resultado:
 

Qual é exatamente o resultado que queremos alcançar. Protegemos o método WriteCustomSentence (nosso recurso personalizado) do assembly parcialmente confiável (que é o código SecurityTransparent), permitindo que o mesmo assembly acesse o método WriteDefaultSentence.

Regras de herança e substituição

Vimos como a proteção de recursos funciona quando um método chama outro, mas as verificações de segurança realizadas nessas situações não são suficientes para atingir um conjunto completo de instrumentos de segurança. Por exemplo, sabemos que linguagens orientadas a objetos, como as fornecidas com o .NET, permitem herança e substituição de classes, métodos e tipos. Portanto, precisamos proteger esses mesmos objetos com uma versão derivada da mesma estrutura de herança. O novo sistema de segurança de acesso ao código do .NET Framework 4.0 gerencia essa necessidade usando as duas regras a seguir:
1)	Os tipos derivados devem ser pelo menos tão restritivos quanto os tipos base.
2)	Métodos derivados não podem modificar a acessibilidade de seus métodos base.

Os métodos derivados são SecurityTransparent por padrão e, portanto, se o método base não for SecurityTransparent, o derivado deverá ser marcado adequadamente para evitar a violação da primeira regra de herança.

Para demonstrar as duas regras, escreveremos uma classe CasWriterInheritance que herda da classe CasWriter e teremos um método WriteCustomSentence que herda do método WriteCustomSentence base (que marcamos como virtual). O código para isso será:

namespace CasWriterTeste
{
    public class CasWriterInheritance : CasWriter
    {
        [SecurityCritical()]
        public override void WriteCustomSentence(string text)
        {
            base.WriteCustomSentence(text);
        }
    }
}

Para demonstrar a primeira regra de herança e substituição, definiremos a classe CasWriter como SecurityCritical:

[SecurityCritical()]
public classCasWriter
{

E, no método principal do assembly CasWriterTeste.exe, substituiremos o objeto CasWriter pelo objeto CasWriterInheritance:

static void Main(string[] args)
{
    CasWriter writer = new CasWriterInheritance();
    Console.WriteLine(writer.GetMethodsSecurityStatus());

Portanto, tentamos derivar a classe SecurityTransparent CasWriter2 de uma classe SecurityCritical CasWriter, mas, com a primeira regra em vigor, isso não é possível porque tentamos criar um tipo SecurityTransparent (com baixa proteção) de um SecurityCritical (com alta proteção ) tipo. Como resultado, se executarmos nosso .exe, obteremos:
 

Como esperado, uma exceção de carregamento de tipo é lançada, informando que uma regra de segurança de herança foi violada. Observe que o exe também pára de funcionar; porque a exceção é detectada quando o assembly carrega o tipo CasWriterInheritance, não é possível lidar com a exceção por meio do código.

Para deixar isso o mais claro possível, a tabela a seguir resume as regras de herança para classes:
Classe Base	Classe Derivada
Transparent	Transparent
Transparent	SafeCritical 
Transparent	Critical
SafeCritical 	SafeCritical 
SafeCritical	Critical
Critical	Critical

Para demonstrar a segunda regra, removeremos o atributo SecurityCritical da classe CasWriter. Nesse caso, as primeiras regras não são mais violadas, pois as duas classes são SecurityTransparent. No entanto, há uma segunda questão a considerar; estamos tentando substituir o código SecurityCritical (a base WriteCustomSentence) pelo código agora SecurityTransparent (a derivada WriteCustomSentence), que não é permitido pela segunda regra. Lembre-se de que o método derivado é SecurityTransparent por padrão e não especificamos nenhum outro atributo de segurança para ele. Ao executar o .exe, obtemos:
 

Como esperado, uma exceção é lançada, dizendo que há uma violação de uma regra de segurança ao substituir o WriteCustomSentence. Deixo a você marcar o método WriteCustomSentence da classe CasWriterInheritance como SecurityCritical e verificar se, nesta última situação, tudo vai bem. Para experimentá-lo, você pode fazer o download do arquivo zip de suporte na parte superior da página, que contém todo o exemplo fornecido neste artigo. Antes de terminarmos de analisar os métodos, vamos confirmar suas regras de herança:
Classe Base	Classe Derivada
Transparent	Transparent
Transparent	SafeCritical 
SafeCritical 	Transaprent
SafeCritical 	SafeCritical
Critical	Critical

//[SecurityCritical()]
public class CasWriterInheritance : CasWriter
{
    [SecurityCritical()]
    public override void WriteCustomSentence(string text)
    {
        base.WriteCustomSentence(text);
    }
}
 
Encerro esta seção apontando que as mesmas regras se aplicam quando desenvolvemos uma classe que implementa uma interface. O método implementado deve respeitar as regras de herança (as mesmas da tabela acima) em relação aos atributos configurados para os membros da interface.
A ferramenta .NET Security Annotator.

No exemplo anterior, vimos como combinar os atributos SecurityCritical e SecuritySafeCritical para proteger o método WriteCustomSentence do código parcialmente confiável. É certo que esse exemplo foi muito fácil e definir os atributos corretos foi uma tarefa trivial. As coisas não são tão fáceis com montagens mais complexas e existe o risco de criar confusão quando você tenta desvendar as dependências de segurança. É exatamente por isso que o .NET Framework 4.0 da Microsoft fornece uma ferramenta muito útil, denominada .NET Security Annotator (SecAnnotate.exe), que pode ajudar os desenvolvedores a identificar os atributos corretos a serem usados no código deles. Você pode encontrá-lo no Microsoft Windows SDK versão 7.0A, na pasta \bin\ NETFX 4.0 Tools.

A ferramenta SecAnnotate.exe navega em um assembly para identificar quais modificações precisam ser feitas para evitar exceções de segurança quando o assembly é executado, e as verificações são feitas em várias passagens. Na primeira passagem, a ferramenta descobre quais modificações devem ser executadas na montagem como ela existe inicialmente. Se ele detecta que algum código deve ser marcado como SecurityCritical ou SecuritySafeCritical, ele executa uma segunda passagem, aplicando, em tempo de execução, as modificações descobertas necessárias na primeira passagem. A ferramenta faz uma terceira passagem e, se detectar que são necessárias novas modificações como resultado das alterações anteriores, faz essas modificações na quarta passagem. O processo se repete (varredura - modificação - varredura - modificação ...) e termina quando a ferramenta não encontra mais nada para mudar. No final da execução, o SecAnnotation.exe gera um relatório de saída que contém o resultado da análise realizada em cada etapa.

Há duas coisas que você deve ter em mente:
1)	Se o SecAnnotate.exe descobrir que um método deve ser marcado como SecurityCritical ou SecuritySafeCritical, ele prefere o primeiro atributo, sendo uma opção mais segura. Às vezes, os desenvolvedores precisam selecionar manualmente o atributo SecuritySafeCritical em vez de SecurityCritical, e isso pode gerar problemas durante aspassagens seguintes. Veremos um exemplo do que quero dizer em um momento. Para evitar isso, a ferramenta SecAnnotate.exe vem com a opção de linha de comando /p: <n>, que pode ser usada para definir o número máximo de passagens que podem ser executadas antes de interromper a execução e gerar a saída. Em termos de um processo mais controlado, que permite que você assuma o controle direto e refinado da segurança do seu código, seria melhor:
a)	executar a ferramenta com a opção de linha de comando /p: 1 para que, a cada passo, uma nova saída seja gerada;
b)	executar manualmente as modificações desejadas na montagem com base nessa saída,
c)	recompile sua montagem e
d)	Execute novamente o SecAnnotate.exe com a opção de linha de comando /p: 1 para obter uma nova saída e repita. O procedimento termina quando nenhuma outra modificação é necessária, como quando você permite que o SecAnnotate.exe seja executado sem intervenção humana.

2)	Para executar a verificação, a ferramenta SecAnnotate.exe precisa verificar como os métodos do assembly se comportam em relação aos métodos que eles chamam. Geralmente, os assemblies usam as classes base do .NET Framework e, portanto, podem ser realizadas verificações sobre os atributos necessários para chamar seus métodos. Se um assembly usar outros assemblies (de terceiros ou seus), diferentes dos presentes nas classes base do .NET Framework (e, em geral, daqueles contidos no Global Assembly Cache), o caminho para eles deverá ser especificado. d: opção de linha de comando <diretório>.

Com tudo isso em mente, se retornarmos ao nosso assembly CasWriterDemo.dll, remova os atributos de segurança definidos na seção anterior.

//[assembly: SecurityTransparent()]
namespace CasWriterTeste

E

//[assembly: AllowPartiallyTrustedCallers()]
namespace CasWriterDemo
{
    //[SecurityCritical()]
    public class CasWriter
    { 
        [SecurityCritical()]
        public virtual void WriteCustomSentence(string text)
 {

        [SecuritySafeCritical()]
        public void WriteDefaultSentence(int index)
        {
 

Em seguida, digite o seguinte comando no console:

SecAnnotate.exe CasWriterDemo.dll 

Obteremos a seguinte saída:
 

Parâmetros	Descrição
/a or /showstatistics	Mostrar estatísticas sobre o uso da transparência nas montagens que estão sendo analisadas.
/d:directory or /referencedir:directory	Inclua o diretório especificado ao procurar montagens dependentes durante a anotação.
/i or /includesignatures	Inclua informações de assinatura estendidas no arquivo de relatório de anotação.
/n or /nogac	 Suprima a procura de assemblies referenciados no cache global de assemblies.
/o:output.xml or /out:output.xml	Especifica o nome do arquivo de anotação de saída.
/p:maxpasses or /maximumpasses:maxpasses	Especifique o número máximo de passes de anotação a serem feitos nas montagens antes de interromper a geração de novasanotações.
/q or /quiet	Modo silencioso: o anotador gera apenas informações de erro e nenhuma mensagem de status.
/r:assembly or /referenceassembly:assembly	Inclua a montagem especificada ao resolver montagens dependentes durante a anotação. Os conjuntos de referência sãodada prioridade sobre os assemblies encontrados no caminho de referência.
/s:rulename or /suppressrule:rulename	Suprima a execução de uma regra de transparência nos conjuntos de entrada.
/t or /forcetransparent	Força o Anotador a tratar todas as montagens sem nenhuma anotação de transparência como se fossem totalmente transparentes.
/t:assembly or /forcetransparent:assembly	Force a montagem especificada a ser transparente, independentemente de suas anotações atuais no nível da montagem.
/v or /verify	Verifique se as anotações de uma montagem estão corretas apenas, não tenta fazer várias passagens para encontrar todas as anotações necessárias se a montagem não verificar.
/x or /verbose	Saída detalhada durante a anotação
/y:directory or /symbolpath:directory	Inclua o diretório especificado ao procurar arquivos de símbolo durante a anotação.

A ferramenta não encontra nada para anotar, porque o assembly é composto pelo código SecurityTransparent que chama outro código SecurityTransparent (especificamente, o código das classes base do .NET Framework que usamos).Mas, se quisermos proteger o método WriteCustomSentence, marcando-o como SecurityCritical (como fizemos anteriormente), voltaríamos a saída:
 

E, se lançarmos o comando anterior no assembly recém-compilado, obteremos um resultado diferente:
 

Podemos ver que a ferramenta encontrou três anotações necessárias e os trabalhos foram concluídos em duas passagens. Além disso, gerou um relatório detalhado intitulado TransparencyAnnotations.xml (podemos substituir o nome pela opção SecAnnotate.exe /o:filename.xml CasWriterDemo.dll), cujo conteúdo se parece com o seguinte:
 
Podemos ver rapidamente que a ferramenta SecAnnotation.exe fez uma anotação no método WriteDefaultSentence, por três razões idênticas. A regra violada é fornecida por TransparentMethodsMustNotReferenceCriticalCode, como esperávamos. Os três motivos são todos idênticos porque o método SecurityTransparent WriteDefaultSentence contém três chamadas para o método SecurityCritical WriteCustomSentence (dentro do bloco de código do comutador).

<reason pass="1">
Transparent method 'CasWriterDemo.CasWriter.WriteDefaultSentence(System.Int32)' 
references security critical method 'CasWriterDemo.CasWriter.WriteCustomSentence(System.String)'.  
In order for this reference to be allowed under the security transparency rules, either 'CasWriterDemo.CasWriter.WriteDefaultSentence(System.Int32)' must become security critical or safe-critical, or 'CasWriterDemo.CasWriter.WriteCustomSentence(System.String)' become security safe-critical or transparent.
</reason>

Outro aspecto importante deste relatório a ser observado é que a ferramenta sugere quatro maneiras diferentes de evitar a anotação:
1)	WriteDefaultSentence deve se tornar SecurityCritical
2)	WriteDefaultSentence deve se tornar SecuritySafeCritical
3)	WriteCustomSentence deve se tornar SecuritySafeCritical
4)	WriteCustomSentence deve se tornar SecurityTransparent

Se todos eles puderem resolver o problema completamente, sabemos que, para os objetivos que temos em mente, a única solução disponível é tornar o WriteDefaultSentence SecuritySafeCritical, a fim de conceder acesso a ele pelo código SecurityTransparent, deixando o método WriteCustomSentence protegido. Também sabemos que a ferramenta, após a primeira passagem, aplica a regra que considera preferível ao realizar sua segunda passagem e que prefere alterações que tragam a melhor situação de segurança possível.

Em nosso exemplo, ele pode ter escolhido a opção número 1 e, como resultado, o assembly se tornaria totalmente SecurityCritical e, portanto, completamente protegido do código SecurityTransparent. Isso representa a situação mais segura. No entanto, sabemos que, para nossos objetivos, a solução de que precisamos é o número 2. 

[SecuritySafeCritical()]
publicvoid WriteDefaultSentence(int index)
 

De fato, a aplicação da opção número 1 em vez do número 2 pode gerar outra rodada de verificações que podem ter resultados totalmente diferentes, enviando SecAnnotate.exe ainda mais e mais longe do resultado desejado. Portanto, como mencionei, provavelmente devemos usar as ferramentas com a opção de linha de comando /p: 1 e fazer as alterações manualmente.

Terminaremos esta seção executando a ferramenta SecAnnotatio.exe no aplicativo do console, apenas para ver o que acontece. 

SecAnnotate.exe CasWriterTeste.exe 

Caso precise especificar o local do assembly CasWriterDemo.dll do qual o CasWriterTeste.exe dependeprecisamos usar a opção de linha de comando /d; supondo que o CasWriterDemo.dll esteja contido na raiz da unidade D:\, precisamos executar o seguinte comando:

SecAnnotate.exe /d:D:\CasWriterTeste.exe 

A saída que obtemos é vista abaixo:
 

Podemos ver rapidamente que a ferramenta encontrou apenas uma anotação, que podemos ver no relatório anexo:
 
<reason pass="1">
Transparent method 'CasWriterTeste.Program.Main(System.String[])' 
references security critical method 'CasWriterDemo.CasWriter.WriteCustomSentence(System.String)'.  
In order for this reference to be allowed under the security transparency rules, either 'CasWriterTeste.Program.Main(System.String[])' must become security critical or safe-critical, or 'CasWriterDemo.CasWriter.WriteCustomSentence(System.String)' become security safe-critical or transparent.
</reason>

A anotação está relacionada ao método Main, que é SecurityTransparent e está tentando acessar o código SecurityCritical. Observe que isso não é uma exceção, mas o comportamento que desejamos implementar em nosso CasWriterDemo.dll para proteger WriteCustomSentence do código SecurityTransparent (como o método Main). Portanto, ao usar esta ferramenta, analise a saída gerada com muito cuidado e atenção.

Além de tudo isso, há um último ponto importante a considerar. Escrevemos dois assemblies, CasWriterTeste.exe e CasWriterDemo.dll relacionados, e se queremos usar a ferramenta SecAnnotation.exe para verificar as regras do CAS para toda a solução, simplesmente não podemos fazê-lo em uma única etapa. No último exemplo, analisamos o assembly CasWriterTeste.exe, especificando seu assembly CasWriterDemo.dll mencionado. No entanto, a partir da saída obtida, é claro que as verificações foram feitas apenas para o assembly CasWriterTeste.exe e como ele se comporta em relação ao assembly CasWriterDemo.dll dependente. Nenhuma verificação foi feita no assembly CasWriterDemo.dll (se você não percebeu no momento, as três anotações relacionadas ao assembly CasWriterDemo.dll não estão presentes no relatório posterior).

O que estou tentando enfatizar é que, se você quiser verificar toda a sua solução, precisará executar a verificação para cada montagem, uma de cada vez. A menos que você tenha objetivos de segurança específicos em mente, a melhor maneira é verificar o conjunto dependente primeiro e depois os chamadores imediatos.

Vamos terminar este artigo com algumas reflexões sobre como configurar uma estratégia de proteção bem-sucedida ao trabalhar com o novo modelo Level2 SecurityTransparence. Podemos definir duas situações diferentes em que provavelmente nos encontraremos:
1)	Nosso assembly deve proteger os assemblies dependentes subjacentes (por exemplo, as classes base do .NET Framework). Nesse caso, precisamos maximizar a quantidade de código SecurityCritical. Dessa forma, somos capazes de proteger todos os conjuntos dependentes dos conjuntos parcialmente confiáveis (SecurityTransparent) com uma "parede" impenetrável.
2)	Nosso assembly será protegido por seus potenciais chamadores. Se nosso assembly precisar ser acessado por um assembly parcialmente confiável, precisamos maximizar a quantidade de código SecurityTransparent. Se o assembly não usar recursos protegidos, precisamos marcá-lo apenas como SecurityTransparent; caso contrário, devemos usar os atributos APTCA e tentar, método por método, maximizar o código SecurityTransparent e minimizar o código SecuritySafeCritical.

Obviamente, não é tão fácil adivinhar todo o espectro de cenários possíveis e verificar se as duas regras acima são aplicáveis a todos eles; portanto, essas duas devem ser consideradas diretrizes gerais. 

Classe SecureString

Ao trabalhar com senhas ou números de cartão de crédito normalmente usamos a classe ou o tipo string para armazenar ou trabalhar com elas. Mas a implementação padrão System.String não é otimizada para segurança. O uso de uma string para armazenar dados confidenciais tem alguns problemas:
•	O valor da sequência pode ser movido na memória pelo coletor de lixo, deixando várias cópias ao redor.
•	O valor da sequência não é criptografado. Se você ficar com pouca memória, pode ser que sua string seja gravada como texto sem formatação em um arquivo de paginação no disco. O mesmo pode acontecer quando o aplicativo falha e é feito um despejo de memória.
•	System.String é imutável. Cada alteração fará uma cópia dos dados, deixando várias cópias na memória.
•	É impossível forçar o coletor de lixo a remover todas as cópias da sua string da memória.

Em tal situação, o C # fornece a classe SecureString (no namespace System.Security) para trabalhar com suas strings que necessitam de maior segurança. 

O SecureString criptografa automaticamente a string e a armazena em um local de memória especial. É mutável e implementado pelo IDisposable; é por isso que não há problema de várias cópias de dados e a impossibilidade de um coletor de lixo para limpar todas as cópias. Sempre que você terminar de trabalhar com o SecureString, verifique se o conteúdo foi removido da memória usando IDisposable. O coletor de lixo não move a string, assim você evita o problema de ter várias cópias. SecureString é uma sequência mutável que pode ser feita somente leitura quando necessário. 

Um SecureString não resolve completamente todos os problemas de segurança. Como ele precisa ser inicializado em algum momento, os dados usados para inicializar o SecureString ainda estão na memória, mas minimiza o risco de comprometimento dos dados, pois manipula a string caractere por caractere e não a string inteira. Não é possível passar uma string diretamente para um SecureString.Quando necessário, você pode tornar a sequência criptografada pelo SecureString como somente leitura.

public static SecureString InputSecureString()
{
    using (SecureString secureString = new SecureString())
    {
        Console.Write("Please enter your password/Credit Card Number: ");
        while (true)
        {
            ConsoleKeyInfo enteredKey = Console.ReadKey(true);
            if (enteredKey.Key == ConsoleKey.Enter)
                break;
            secureString.AppendChar(enteredKey.KeyChar);
            Console.Write("#");
        }
        secureString.MakeReadOnly();

        return secureString;
    }
}

Como você pode ver, o SecureString é usado com uma instrução using, portanto o método Dispose é chamado quando você termina a string, para que não fique na memória por mais tempo do que o estritamente necessário.

Em algum momento, você provavelmente desejará converter o SecureString novamente em uma sequência normal para poder usá-lo. Você também pode ler a string criptografada (por SecureString) usando a classe especial Marshal, que pode ser encontrada em System.Runtime.InteropServices. A leitura da string criptografada faz com que a string seja descriptografada e a retorne como uma string normal (texto sem formatação).

É importante garantir que a sequência regular seja apagada da memória o mais rápido possível. Portanto, encapsule o código de leitura com o bloco try/catch/finally. A instrução finally garante que a cadeia seja removida da memória, mesmo se houver uma exceção lançada no código. O código abaixo mostra um exemplo de como fazer isso.

public static void ConvertToUnsecureString(SecureString secureString)
{
    IntPtr unmanagedString = IntPtr.Zero;

    try
    {
        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
        Console.WriteLine(Marshal.PtrToStringUni(unmanagedString));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        Console.WriteLine("Memory Cleared.");
    }
}

A classe Marshal fornece um método para descriptografar a string, juntamente com um método para limpar o conteúdo da string descriptografada da memória. O método SecureStringToGlobalAllocUnicode() é estático e é usado para ler a string segura e retornar o endereço de um local de memória que contém o valor como IntPtr (ponteiro). Esse ponteiro contém o endereço da localização da memória e é convertido em uma sequência (valor) que o ponteiro contém (aponta para). O método ZeroFreeGlobalAllocUnicode() também é estático e é usado junto com o método SecureStringToGlobalAllocUnicode() para liberar o conteúdo da sequência descriptografada da memória.

O System.Runtime.InteropServices oferece cinco métodos que podem ser usados quando você está descriptografando um SecureString. Esses métodos aceitam um SecureString e retornam um IntPtr. Cada método possui um método correspondente que você precisa chamar para zerar o buffer interno. 
Método de descriptografia	Método de limpeza de memória
SecureStringToBSTR	ZeroFreeBSTR
SecureStringToCoTaskMemAnsi	ZeroFreeCoTaskMemAnsi
SecureStringToCoTaskMemUnicode	ZeroFreeCoTaskMemUnicode
SecureStringToGlobalAllocAnsi	ZeroFreeGlobalAllocAnsi
SecureStringToGlobalAllocUnicode	ZeroFreeGlobalAllocUnicode

É importante perceber que um SecureString não é completamente seguro. Você pode criar um aplicativo, executando em full thrust, capaz de ler o conteúdo do SecureString. No entanto, isso aumenta a complexidade de invadir seu aplicativo. Todas as pequenas etapas que você pode executar para tornar seu aplicativo mais seguro criarão um obstáculo maior para um invasor.

•	Gerenciar montagens / assemblies
o	Controlar versão de montagens; assinar montagens usando nomes fortes; implementar hospedagem lado a lado; colocar uma montagem no GAC; criar uma montagem WinMD

Common Language Runtime (CLR)

O Common Language Runtime (CLR) (linguagem comum em tempo de execução) é o mecanismo responsável pela execução das aplicações do .NET Framework. Pense nele como o agente que gerencia o código em tempo de execução, oferecendo serviços, como o gerenciamento de memória. O C# suporta CLR, assim como outras linguagens de programação da Microsoft. O código gerado pelo compilador para o suporte CLR é denominado código gerenciado.  Veja os benefícios que o CLR proporciona:
•	Gerenciamento automático de memória.
•	Verificação de segurança de tipos.
•	Gerenciamento de exceções.
•	Segurança aprimorada.
•	Acesso a metadados.

Common Intermediate Language - CIL 

Os assemblies contém o código compilado, que anteriormente era conhecido como Microsoft Intermediate Language - MSIL, mas agora é Common Intermediate Language – CIL, fornecendo ao Common Language Runtime as informações de que ele precisa para estar ciente das implementações de tipo. Quando compilamos o código gerenciado, geramos CIL, o qual é independente de CPU e pode ser convertido para código nativo. O CIL está disponível em um arquivo executável portátil (PE) e inclui instruções para carregar, armazenar, inicializar e executar métodos, assim como instruções para operações aritméticas e lógicas, controle de fluxo, etc que ajuda na execução de um assembly. 

O código contido no CIL não pode ser executado diretamente, antes, é preciso convertê-lo para instruções que possam ser interpretadas pela CPU. A conversão é realizada por um compilador Just-In-Time (JIT ou JITter). O CIL não necessita de plataforma, logo só precisamos de um compilador para converter o código CIL em código nativo na máquina-alvo. 

Quando um compilador converte o código-fonte em código CIL, ele também cria seus metadados. Os metadados armazenam informações sobre os dados armazenados em uma montagem. Por exemplo, ele contém informações dos tipos disponíveis nesse assembly, os namespaces, classe base de cada tipo disponível, as interfaces implementadas, seus métodos e seu escopo, os parâmetros de cada método, as propriedades de cada tipo e assim por diante. Em outras palavras, os metadados são a documentação criptografada de um código-fonte. Os metadados, que representam informações utilizadas pelo CLR, são colocados também no arquivo PE, que pode ter a extensão DLL ou EXE.

Quando um código é compilado com êxito, um arquivo de manifesto do assembly é gerado. É um arquivo XML que contém informações sobre o assembly, como nome, número da versão e um nome forte opcional que identifica exclusivamente o assembly. Ele também contém os nomes de outros assemblies de referência usados no código.

Como forma de exemplicar a transformação do código C# de alto-nível em código CIL, foi criada uma amostra mínima de programa,com uso de tipos básicos primitivos e métodos estáticos, que apresenta uma saída visível sem precisar de ligação externa a nenhuma biblioteca existente. Esse código mínimo executa diretamente sobre o hardware, como se fosse um sistema operacional muito simplesde modo que não seja necessária a criação de uma infra-estrutura de instanciação e gerenciamento de objetos durante a execução.O código pode ser visualizado abaixo.

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

O programa VerySimpleKernelconsiste de uma classe estática Program que exibe a letra “A” no vídeo em modo texto de um PC. Elecarrega o byte ASCII correspondente à letra “A” na primeira posição da memória de vídeo (endereço B8000) e o byte correspondente à cor branca no endereço subseqüente. Para manter a saída na tela indefinidamente, um laço infinito é colocado no final do método Main(). Desconsiderando as diretivas, o código CIL gerado (aberto pelo ILdasm) que faz com que o VerySimpleKernel possua quinze instruções,tendo nove delas distintas: 
 

Instrução	Descrição	Efeito POP	Efeito PUSH	Operando “Inline”	Controle de Fluxo
add	Instruções polimórficas para as operações aritméticas de soma.  	Pop1+Pop1	Push1	InlineNone	NEXT
br.s	Desvio incondicional.	Pop0 	Push0 	ShortInlineBrTarget	BRANCH
conv.i
	Converte  o  valor  presente  no  topo  da evaluation stackpara o tipo t especificado no opcode (i1, i2, i4, ..., i, u) e deixa   o   valor   convertido   no   topo.	Pop1 	PushI 	InlineNone 	NEXT
ldc.i4.1
ldc.i4
ldc.i4	A instrução ldc especifica um tipo t (i4, i8, r4 ou r8) para o valor e a constante é codificada “inline” com a instrução.	Pop0 	PushI 	InlineI 	NEXT
ldloc.0	Carrega a variável local de índice indx na evaluation stack.	Pop0 	Push1 	InlineNone 	NEXT
stind.i1	Armazena   um   valor   no   endereço   especificado,   ambos presentes na evaluation stack.	PopI+PopI 	Push0 	InlineNone 	NEXT
stloc.0	Armazena  um  valor  da evaluation  stack  na  variável  local indicada pelo índice indx.	Pop1 	Push0 	InlineNone 	NEXT

Compilador Just-In-Time (JIT)  

Antes de executar o CIL, é preciso utilizar o .NET Framework Just-In-Time (JIT) para convertê-lo para o código nativo. Assim, geramos código específico para a arquitetura na qual roda o compilador JIT. Seguindo esse raciocínio, podemos desenvolver uma aplicação e convertê-la para várias plataformas. Precisamos apenas converter o CIL para o código nativo com um compilador JIT, específico para a plataforma desejada. Cada sistema operacional pode ter seu compilador JIT. Claro que chamadas específicas a API do Windows não funcionará em aplicações que estejam rodando em outro sistema operacional. Isso significa que devemos conhecer e testar muito bem uma aplicação, antes de disponibilizá-la para múltiplas plataformas. Em uma aplicação comercial grande, geralmente usamos um número limitado de funções. Assim, algumas partes do código dessa aplicação podem não ser executadas. Visto que acarreta consumo de tempo e memória, a conversão do CIL para o código nativo é realizada somente na primeira vez em que o código é executado. Por exemplo, se o nosso programa compila um determinado método, haverá compilação somente na primeira vez em que o método for executado. As chamadas seguintes utilizarão o código nativo. O CIL convertido é usado durante a execução e armazenado para que esteja acessível para chamadas subsequentes.

Imagine, por exemplo, que você tenha uma classe com cinco métodos; quando chamar o primeiro método, somente este será compilado; quando precisar de outro método, este também será compilado. Chegará um momento em que todo o CIL estará em código nativo.

ASSEMBLY

Os Assemblies são a parte fundamental da programação com .NET Framework, pois contêm o código que o CLR executa. Uma montagem pode ser uma montagem de arquivo único ou uma montagem de múltiplos arquivos. Como o Visual Studio pode gerar apenas assemblies de arquivo único, não será obordado aqui os assembly de múltiplos arquivos.

Criando Assembly

No .NET Core e .NET Framework, você pode criar um assembly a partir de um ou mais arquivos de código-fonte. No .NET Framework, os assemblies podem conter um ou mais módulos. Isso permite que projetos maiores sejam planejados para que vários desenvolvedores possam trabalhar em módulos ou arquivos de código-fonte separados, que são combinados para criar um único assembly. 

Você pode usar as ferramentas de desenvolvimento, como o Visual Studio criando-os com ferramentas de interface de linha de comando do .NET Core ou compilando assemblies de .NET Framework com um compilador. Você pode também usar ferramentas no SDK do Windows para criar assemblies com módulos de outros ambientes de desenvolvimento.

O CLR possui um componente chamado compilador Just In Time (JIT) que compila o código IL em código binário para executá-lo em uma plataforma específica, como o Windows. O assembly resultante, um código CIL (Common Intermediate Language) dentro de um arquivo Windows Portable Executable (executáveis ou binários PE)  são as unidades fundamentais de implantação, controle de versão, reutilização, escopo de ativação e permissões de segurança para os aplicativos baseados em rede. 

O PE tem essa denominação devido ao padrão estabelecido pela Microsoft nos primórdios do Windows, onde decidiram criar um formato binário (idioma comum que o Common Language Runtime - CLR entende) capaz de rodar em qualquer outra versão do Windows que permanece inalterado desde Windows 95. A estrutura geral de um arquivo PE é apresentada na imagem abaixo:
Arquivo PE
Sub do MS-DOS	Cabeçalho do MS-DOS
	Aplicação DOS 16-bits
Cabeçalho de Arquivo
	Cabeçalho COFF
Cabeçalho Opcional	Campos básicos
	Campos especifícos
	Diretórios de dados
Cabeçalhos de seções
Seções	Seção 0
	Seção n

Isso significa que o padrão/formato de todos esses arquivos é semelhante, variando apenas alguns pequenos detalhes. Assim:
•	COMPILADORES, os programas que criam estes programas, precisam respeitar tal formato 
•	LOADER, o programa que os interpreta, carrega e inicia sua execução precisa entendê-lo. 

Um assembly pode ser um conjunto de processos (arquivos EXE) , assemblies de biblioteca (arquivos DLL - Dynamic Link Library), componentes ActiveX (OCX), entre diversos outros. Um assembly EXE, diferente de um DLL, deve ter um, e somente um, ponto de entrada principal ( DllMain, WinMain ou Main), que indica qual método deve ser chamado quando o aplicativo é iniciado.  Um Assembly pode ser de dois tipos:
•	Private Assembly (privativo): Um assembly que é usado somente por uma única aplicação é chamado de private assembly (assembly privado). Vamos supor que você criou uma DLL que encapsula a sua lógica de negócios. Esta DLL será usada somente pela sua aplicação cliente. Afim de executar a aplicação a sua DLL deve estar no mesmo diretório na qual a aplicação cliente está instalada, então é armazenado na pasta do aplicativo sendo usada apenas por este aplicativo. Se outro aplicativo tentar referenciar um assembly privado, ele deverá armazenar uma cópia desse assembly privado em seu diretório raiz, caso contrário, o aplicativo não poderá implantar com êxito.
•	Public/Shared Assembly (compartilhado): também conhecido como assembly de nome forte é uma DLL de propósitos gerais que fornecerá funcionalidades que serão usadas por várias aplicações, ao invés de você copiar a DLL para cada cliente que vai usar a sua DLL você pode por a DLL em um local de acesso global como o Global Assembly Cache (GAC). 

Antes da Microsoft lançar o .NET Framework, o COM (Component Object Model) era dominante, os Assemblies ainda possuem a extensão .DLL ou  EXE como os componentes anteriores do Windows. Internamente, no entanto, eles são completamente diferentes. Ao contrário de uma DLL antiga foram projetados para simplificar o desenvolvimento de aplicações e resolver os problemas ocorridos, tais como:
1.	Conflito de versões/"DLL Hell": O problema ocorria quando aplicativos diferentes usam a mesma biblioteca, se eles fossem compilados em uma versão diferente da mesma biblioteca, a que foi instalada por último substituirá a DLL existente. Se houvesse problemas de compatibilidade entre diferentes versões do mesmo assembly, um aplicativo começaria a agir de maneira irregular ou até se recusaria a iniciar. Isso ocorria pois a Microsoft e outras empresas de software distribuíam uma nova versão de uma DLL como blocos de construção usadas por outros aplicativos sem testá-la completamente em todos os aplicativos que dependem dela.  
2.	Instalação muito complexa: um aplicativo precisava fazer alterações em várias partes do seu sistema. Além dos diretórios de aplicativos copiados para o seu sistema era necessário fazer muitas alterações no registro e implementações de atalhos. Isso tornava o processo de instalação difícil, pois quando havia processos de desinstalação de um aplicativo, às vezes, deixava rastros de um aplicativo para trás.
3.	Segurança. Como os aplicativos faziam várias alterações no sitema durante a instalação, era difícil para um usuário determinar o que realmente foi instalado. Era possível que um aplicativo, instalasse outros componentes que fossem risco à segurança sem que usuário pudesse identificar a fonte do risco.

O .NET Framework soluciona esses problemas e tenta resolvê-los fazendo algumas mudanças radicais. Um componente importante dessas mudanças é o conceito assembly. Apesar de anteriormente existirem DLLs, internamente, elas são completamente diferentes:
•	Completamente independentes: eles não precisam gravar nenhuma informação no registro ou em outro local, pois todas as informações necessárias para execução estão contida no próprio assembly, no  manifesto da assembléia. A identidade de cada tipo inclui o nome do assembly no qual ele reside. Um tipo chamado MyType, carregado no escopo de um assembly, não é o mesmo de um tipo chamado MyType, carregado no escopo de outro assembly.
•	Neutro em termos de linguagem: Você pode escrever algum código C#, compilá-lo em um assembly e usá-lo diretamente de outras linguagens .NET, como F# ou Visual Basic.
•	Pode ser versionado: Cada aplicação tem suas próprias DLLs. Quando instalamos uma aplicação .NET, os arquivos PE (DLL e EXE) ficam no mesmo diretório da aplicação, isso permite que você tenha versões diferentes de um assembly específico em um sistema sem causar conflitos. 
O assembly é a menor unidade com controle de versão no Common Language Runtime. Ao falar da versão de um componente você estamos falando sobre a versão do assembly para a qual o componente pertence. Todos os tipos e recursos no mesmo assembly têm controle de versão como uma unidade. O manifesto do assembly descreve as dependências de versão que você especifica para quaisquer assemblies dependentes.
•	Facéis de implementar: Se desejar, você pode implantar um aplicativo simplesmente copiando-o para uma nova máquina. Todos os assemblies necessários são implantados localmente na nova pasta do aplicativo. Quando um aplicativo é iniciado, somente os assemblies que o aplicativo chama inicialmente devem estar presentes. Outros assemblies que contêm recursos de localização ou classes utilitárias, podem ser recuperados sob demanda. Isso permite que os aplicativos sejam simples e leves quando baixados pela primeira vez. Isso significa que os assemblies podem ser uma maneira eficiente de gerenciar recursos em projetos grandes.
•	Permite o desenvolvimento baseado em componentes: o que significa que vários assemblies podem reutilizar de maneira compartilhada os tipos, métodos e classes um do outro para criar um produto de software. Uma montagem pode até conter arquivos de recursos, como imagens, diretamente incorporados na montagem. 
O manifesto do assembly tem metadados que são usados para resolver tipos e satisfazer solicitações de recursos. O manifesto especifica os tipos e recursos a serem expostos fora do assembly e enumera outros assemblies dos quais ele depende. O código CIL (Common Intermediate Language) em um arquivo executável portátil (PE) não será executado, a menos que tenha um manifesto de assembly associado.
•	Aprimoram a segurança: que pode ser gerenciada especificando o nível de confiança do código de um site ou zona específico. Um assembly é a unidade na qual as permissões são solicitadas e concedidas.
•	Suporta cultura e idioma; portanto, quando um aplicativo é implantado, ele pode exibir resultados de acordo com uma cultura ou idioma específico.
•	Unidade de execução lado a lado: é a capacidade de executar várias versões de um aplicativo ou componente no mesmo computador.Antes do Windows XP e .NET Framework, conflitos de DLL ocorriam porque os aplicativos eram incapazes de distinguir entre versões incompatíveis do mesmo código.A execução lado a lado e o .NET Framework fornecem os seguintes recursos para eliminar conflitos de DLL:
o	Assemblies com nomes fortes, 
o	Armazenamento de código ciente de versão e 
o	Isolamento.

Assembly dinâmicos

Por definição, os assemblies estáticos são binários do .NET carregados diretamente do armazenamento em disco, o que significa que estão localizados em algum lugar do disco rígido em um arquivo físico (ou possivelmente um conjunto de arquivos no caso de um assembly com vários arquivos) no momento em que o CLR os solicita . Como você pode imaginar, toda vez que você compila seu código-fonte C#, você acaba com um assembly estático.

Um assembly dinâmico é criado na memória rapidamente e não são salvos em disco antes da execução.  Com os tipos fornecidos pelo namespace System.Reflection.Emit permite criar um assembly e seus módulos, definições de tipo e lógica de implementação CIL em tempo de execução e podemos acrescentar os conteúdos dinamicamente ou alterar os já existentes. 

Podemos emitir o código IL diretamente no Assembly, normalmente isto acontece na memória, mas podemos também persistir no Assembly dinâmico salvando seu binário na memória em disco, resultando em uma nova montagem estática. Para ter certeza, o processo de construção de um assembly dinâmico usando o namespace System.Reflection.Emit requer algum nível de entendimento sobre a natureza dos códigos de código CIL.

Embora a criação de montagens dinâmicas seja uma tarefa de programação bastante avançada (e incomum), elas podem ser úteis em várias circunstâncias. Aqui está um exemplo:
•	Criar uma ferramenta de programação .NET que precisa gerar assemblies sob demanda com base na entrada do usuário.
•	Gerar proxies para tipos remotos em tempo real, com base nos metadados obtidos. Por exemplo: Entity Framework usa Reflection.Emit para criar classes de proxy em tempo de execução, herdadas de suas classes de modelo para fornecer carregamento lento e rastreamento de alterações . Também é usado para desenvolver o LinqPad e ajuda a criar dinamicamente Datacontexts.
•	Carregar uma montagem estática e inserir dinamicamente novos tipos na imagem binária.
•	Se possuir suas próprias linguagens de macro, compiladores ou compiladores de scripts em seus aplicativos.
•	Melhorar o desempenho de seus algoritmos criando montagens, classes, módulos e novos tipos durante o tempo de execução.

Dito isto, vamos verificar os tipos em System.Reflection.Emit. Dentro deste namespace existem várias classes que são utilizadas para criarmos dinamicamente um Assembly e seus respectivos tipos.

Essas classes são também conhecidas como builder classes, ou seja, para cada um dos membros como Assembly, Type, Constructor, Event, Property, etc., existem uma classe correspodente com um sufixo em seu nome, chamado XXXBuilder, indicando que é um construtor de um dos itens citados. Para a criação de um Assembly dinâmico, temos as seguintes classes:
Classe	Descrição
AssemblyBuilder	Usado para criar um assembly (* .dll ou * .exe) em tempo de execução. * .exes deve chamar o método ModuleBuilder.SetEntryPoint () para definir o método que é o ponto de entrada para o módulo. Se nenhum ponto de entrada for especificado, uma * .dll será gerada.
ModuleBuilder	Cria e representa um módulo. Usado para definir o conjunto de módulos na montagem atual.
EnumBuilder	Usado para criar um tipo de enumeração .NET.
TypeBuilder	Fornece um conjunto de rotinas que são utilizados para criar classes, interfaces, estruturas e delegados podendo adicionar métodos e campos dentro de um módulo em tempo de execução.
ConstructorBuilder	Define um construtor para uma classe.
EventBuilder	Define um evento para uma classe.
FieldBuilder	Define um campo.
PropertyBuilder	Define uma propriedade para uma determinada classe.
MethodBuilder	Define um método para uma classe.
ParameterBuilder	Define um parâmetro.
GenericTypeParameterBuilder	Define um parâmetro genérico para classes e métodos.
LocalBuilder	Cria uma variável dentro de um método ou construtor.
ILGenerator	Gera código CIL (Common Intermediate Language) em um determinado membro do tipo.
OpCodes	Fornece vários campos que são mapeados para os códigos de código CIL. Esse tipo é usado em conjunto com os vários membros do System.Reflection.Emit.ILGenerator.

Em geral, os tipos do namespace System.Reflection.Emit permitem representar tokens CIL brutos programaticamente durante a construção de seu assembly dinâmico. Você verá muitos desses membros no exemplo a seguir; no entanto, vale a pena conferir o tipo ILGenerator imediatamente.
A função do System.Reflection.Emit.ILGenerator
Como o próprio nome indica, a função do tipo ILGenerator é injetar OpCodes CIL em um determinado membro do tipo. No entanto, você não pode criar objetos ILGenerator diretamente, pois esse tipo não possui construtores públicos; em vez disso, você recebe um tipo de ILGenerator chamando métodos específicos dos tipos centrados no construtor (como os tipos MethodBuilder e ConstructorBuilder). Aqui está um exemplo:

// Create the custom ctor.
Type[] constructorArgs = new Type[1];
constructorArgs[0] = typeof(string);
ConstructorBuilder constructor = helloWorldClass.DefineConstructor(MethodAttributes.Public,
CallingConventions.Standard, constructorArgs);

ILGenerator constructorIL = constructor.GetILGenerator();
constructorIL.Emit(OpCodes.Ldarg_0);

Depois de ter um ILGenerator em suas mãos, você poderá emitir os opcodes brutos do CIL usando qualquer número de métodos. A tabela abaixo documenta alguns métodos (mas não todos) do ILGenerator.
Método	Descrição
BeginCatchBlock ()	Inicia um bloco de captura
BeginExceptionBlock ()	Inicia um escopo de exceção para uma exceção
BeginFinallyBlock ()	inicia um bloco finalmente
BeginScope ()	Inicia um escopo lexical
DeclareLocal ()	Declara uma variável local
DefineLabel ()	Declara um novo rótulo
Emit ()	É sobrecarregado várias vezes para permitir que você emita códigos de operação CIL
EmitCall ()	Envia uma chamada ou código de operação callvirt para o fluxo CIL
EmitWriteLine ()	Emite uma chamada para Console.WriteLine () com diferentes tipos de valores
EndExceptionBlock ()	Termina um bloco de exceção
EndScope ()	Termina um escopo lexical
ThrowException ()	Emite uma instrução para lançar uma exceção
UsingNamespace ()	Especifica o espaço para nome a ser usado na avaliação de locais e relógios para o escopo lexical ativo atual

O principal método do ILGenerator é Emit (), que funciona em conjunto com o System.Reflection. Tipo de classe Emit.OpCodes. Conforme mencionado anteriormente neste capítulo, esse tipo expõe um bom número de campos somente leitura que são mapeados para códigos de operação CIL brutos. O conjunto completo desses membros está documentado na ajuda online e você verá vários exemplos nas páginas a seguir.

Emitindo uma montagem dinâmica

Para ilustrar o processo de definição de um assembly .NET em tempo de execução, vamos percorrer o processo de criação de um assembly dinâmico de arquivo único chamado MyAssembly.dll. Dentro deste módulo, há uma classe chamada HelloWorld. A classe HelloWorld suporta um construtor padrão e um construtor personalizado usado para atribuir o valor de uma variável de membro particular (theMessage) do tipo string. Além disso, o HelloWorld oferece suporte a um método público de instância chamado SayHello(), que imprime uma saudação no fluxo de E/S padrão, e outro método de instância chamado GetMsg(), que retorna a cadeia privada interna. Com efeito, você irá gerar programaticamente o seguinte tipo de classe:

// This class will be created at runtime
// using System.Reflection.Emit.
public class HelloWorld
{
    private string theMessage;
    HelloWorld() { }
    HelloWorld(string s) { theMessage = s; }
    public string GetMsg() { return theMessage; }
    public void SayHello()
    {
        System.Console.WriteLine("Hello from the HelloWorld class!");
    }
}

Suponha que você tenha criado um novo projeto de aplicativo de console do Visual Studio chamado DynamicAsmBuilder e importe os namespaces System.Reflection, System.Reflection.Emit e System.Threading. Defina um método estático chamado CreateMyAsm() na classe Program. Este método único é responsável pelo seguinte:
•	Definindo as características da montagem dinâmica (nome, versão, etc.)
•	Implementando o tipo HelloClass
•	Salvando a montagem na memória em um arquivo físico

Emitindo a montagem e o conjunto de módulos


Você não pode adicionar um tipo a um assembly existente, porque um assembly é imutável após sua criação. Os assemblies dinâmicos não são coletados como lixo e permanecem na memória até o término do processo, a menos que você especifique AssemblyBuilderAccess.RunAndCollect ao definir o assembly. Várias restrições se aplicam a montagens colecionáveis. 

Se for criar um tipo dinamicamente, como um tipo deve residir em um módulo dentro de uma montagem, primeiro precisamos criar a montagem e o módulo antes de podermos criar o tipo. Este é o trabalho dos tipos AssemblyBuilder e ModuleBuilder.

O corpo do método começa estabelecendo o conjunto mínimo de características sobre seu assembly, usando os tipos AssemblyName e Version (definidos no namespace System.Reflection). Em seguida, você obtém um tipo AssemblyBuilder através do método AppDomain.DefineDynamicAssembly () no nível da instância (lembre-se de que o chamador passará uma referência AppDomain para o método CreateMyAsm()), da seguinte maneira:

public static void CreateMyAsm_Comentado(AppDomain curAppDomain)
{
    // Establish general assembly characteristics.
    AssemblyName assemblyName = new AssemblyName();
    assemblyName.Name = "MyAssembly";
    assemblyName.Version = new Version("1.0.0.0");
    // Create new assembly within the current AppDomain.
    AssemblyBuilder assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,
        AssemblyBuilderAccess.Save);
}

Como você pode ver, ao chamar AppDomain.DefineDynamicAssembly (), você deve especificar o modo de acesso do assembly que deseja definir, cujos valores mais comuns são mostrados na tabela abaixo.
Valor	Descrição
ReflectionOnly	Representa que um assembly dinâmico só pode ser refletido
Run	Representa que um assembly dinâmico pode ser executado na memória, mas não salvo no disco.
RunAndSave	Representa que um assembly dinâmico pode ser executado na memória e salvo no disco.
Save	Representa que um assembly dinâmico pode ser salvo no disco, mas não executado na memória.

A próxima tarefa é definir o conjunto de módulos para sua nova montagem. Dado que a montagem é uma unidade de arquivo único, você precisa definir apenas um único módulo. Se você fosse criar um assembly de vários arquivos usando o método DefineDynamicModule (), especificaria um segundo parâmetro opcional que represente o nome de um determinado módulo (por exemplo, myMod.dotnetmodule). No entanto, ao criar uma montagem de arquivo único, o nome do módulo será idêntico ao nome da própria montagem. De qualquer forma, depois que o método DefineDynamicModule () retornar, você receberá uma referência a um tipo válido de ModuleBuilder.

// Given that we are building a single-file
// assembly, the name of the module is the same as the assembly.
ModuleBuilder module = assembly.DefineDynamicModule("MyAssembly", "MyAssembly.dll");

A função do tipo ModuleBuilder

ModuleBuilder é o tipo de chave usado durante o desenvolvimento de montagens dinâmicas. Como seria de esperar, o ModuleBuilder suporta vários membros que permitem definir o conjunto de tipos contidos em um determinado módulo (classes, interfaces, estruturas, etc.), bem como o conjunto de recursos incorporados (tabelas, imagens, etc.) .) contidos dentro. A Tabela abaixo descreve alguns dos métodos centrados na criação. (Observe que cada método retornará para você um tipo relacionado que representa o tipo que você deseja construir.)
Método	Descrição
DefineEnum ()	Usado para emitir uma definição de enum .NET
DefineResource ()	Define um recurso incorporado gerenciado a ser armazenado neste módulo
DefineType ()	Constrói um TypeBuilder, que permite definir tipos de valor, interfaces e tipos de classe (incluindo delegados)

O principal membro da classe ModuleBuilder a ter em atenção é DefineType (). Além de especificar o nome do tipo (por meio de uma sequência simples), você também usará a enumeração System.Reflection.TypeAttributes para descrever o formato do tipo em si. A Tabela abaixo lista alguns (mas não todos) dos principais membros da enumeração TypeAttributes.
Membro	Descrição
Abstract	Especifica que o tipo é abstrato
Class	Especifica que o tipo é uma classe
Interface	Especifica que o tipo é uma interface
NestedAssembly	Especifica que a classe está aninhada com visibilidade de montagem e, portanto, é acessível apenas por métodos em sua montagem
NestedFamANDAssem	Especifica que a classe está aninhada com visibilidade de montagem e família e, portanto, é acessível apenas por métodos situados na interseção de sua família e montagem
NestedFamily	Especifica que a classe está aninhada com a visibilidade da família e, portanto, é acessível apenas por métodos dentro de seu próprio tipo e qualquer subtipo
NestedFamORAssem	Especifica que a classe está aninhada com visibilidade de família ou montagem e, portanto, é acessível apenas por métodos que estão na união de sua família e montagem
NestedPrivate	Especifica que a classe está aninhada com visibilidade privada
NestedPublic	Especifica que a classe está aninhada com visibilidade pública
NotPublic	Especifica que a classe não é pública
Public	Especifica que a classe é pública
Sealed	Especifica que a classe é concreta e não pode ser estendida
Serializable	Especifica que a classe pode ser serializada

Emitindo o tipo HelloClass e a variável de membro String

Depois de termos um módulo no qual o tipo pode residir, podemos usar o TypeBuilder para criar o tipo. Vamos examinar como você pode emitir o tipo de classe pública HelloWorld e a variável de cadeia privada.

// Define a public class named "HelloWorld".
TypeBuilder helloWorldClass = module.DefineType("MyAssembly.HelloWorld",
    TypeAttributes.Public);

O enumerador TypeAttributes suporta os modificadores de tipo CLR que você vê ao desmontar um tipo com ildasm. Além dos sinalizadores de visibilidade dos membros, isso inclui modificadores de tipo como Abstract e Sealed - e Interface para definir uma interface .NET. Ele também inclui Serializable, que é equivalente à aplicação do atributo [Serializable] em C#, e Explicit, que é equivalente à aplicação de [StructLayout (LayoutKind.Explicit)]. 

O método DefineType também aceita um tipo de base opcional:
•	Para definir uma estrutura, especifique um tipo base de System.ValueType.
•	Para definir um delegado, especifique um tipo base de System .MulticastDelegate. Requer várias etapas extras
•	Para implementar uma interface, use o construtor que aceita uma matriz de tipos de interface.
•	Para definir uma interface, especifique TypeAttributes .Interface | TypeAttributes.Abstract.

Observe como o método TypeBuilder.DefineField() fornece acesso a um tipo de FieldBuilder.

// Define a private String member variable named "theMessage".
FieldBuilder msgField = helloWorldClass.DefineField("theMessage",
    Type.GetType("System.String"), FieldAttributes.Private);

A classe TypeBuilder também define outros métodos que fornecem acesso a outros tipos de "construtores". Por exemplo, DefineConstructor () retorna um ConstructorBuilder, DefineProperty() retorna um PropertyBuilder e assim por diante.

PropertyBuilder prop = tb.DefineProperty(
                        "Text", // Name of property
                        PropertyAttributes.None,
                        typeof(string), // Property type
                        new Type[0]); // Indexer types

Observe que não especificamos a visibilidade da propriedade: isso é feito individualmente nos métodos do acessador. Agora podemos criar membros dentro do tipo:

MethodBuilder methBuilder = tb.DefineMethod("SayHello", MethodAttributes.Public, null, null);
ILGenerator gen = methBuilder.GetILGenerator();
gen.EmitWriteLine("Hello world");
gen.Emit(OpCodes.Ret);

Agora estamos prontos para criar o tipo. Depois que o tipo é criado, podemos usar a reflexão comum para inspecionar e executar a ligação tardia:

Type t = tb.CreateType();

object o = Activator.CreateInstance(t);
t.GetMethod("SayHello").Invoke(o, null);

Lembre-se de que você deve chamar CreateType em um TypeBuilder quando terminar de preenchê-lo. Chamar CreateType sela o TypeBuilder e todos os seus membros - para que nada mais possa ser adicionado ou alterado - e devolve um Type real que você pode instanciar. Antes de chamar CreateType, o TypeBuilder e seus membros estão em um estado não criado. Existem restrições significativas sobre o que você pode fazer com construções não criadas. Em particular, você não pode chamar nenhum membro que retorne objetos MemberInfo, como GetMembers, GetMethod ou GetProperty - todos esses lançam uma exceção. Se você deseja se referir a membros de um tipo não criado, deve usar as emissões originais.

Emitindo os construtores

Como mencionado anteriormente, o método TypeBuilder.DefineConstructor () pode ser usado para definir um construtor para o tipo atual. No entanto, quando se trata de implementar o construtor do HelloClass, é necessário injetar código CIL bruto no corpo do construtor, responsável por atribuir o parâmetro recebido à cadeia privada interna. Para obter um tipo ILGenerator, chame o método GetILGenerator () do respectivo tipo de "construtor" ao qual você tem referência (neste caso, o tipo ConstructorBuilder). O método Emit () da classe ILGenerator é a entidade encarregada de colocar o CIL em uma implementação de membro. O próprio Emit () faz uso frequente do tipo de classe OpCodes, que expõe o conjunto de códigos opc do CIL usando campos somente leitura. Por exemplo, OpCodes.Ret sinaliza o retorno de uma chamada de método, OpCodes.Stfld faz uma atribuição a uma variável de membro e OpCodes.Call é usado para chamar um determinado método (nesse caso, o construtor da classe base). Dito isto, considere a seguinte lógica do construtor:

// Create the custom ctor.
Type[] constructorArgs = new Type[1];
constructorArgs[0] = typeof(string);

// Obtain an ILGenerator from a ConstructorBuilder object named "myCtorBuilder".
ConstructorBuilder constructor = helloWorldClass.DefineConstructor(MethodAttributes.Public,
CallingConventions.Standard, constructorArgs);

ILGenerator constructorIL = constructor.GetILGenerator();
constructorIL.Emit(OpCodes.Ldarg_0);

Type objectClass = typeof(object);
ConstructorInfo superConstructor = objectClass.GetConstructor(new Type[0]);
constructorIL.Emit(OpCodes.Call, superConstructor);
constructorIL.Emit(OpCodes.Ldarg_0);
constructorIL.Emit(OpCodes.Ldarg_1);
constructorIL.Emit(OpCodes.Stfld, msgField);
constructorIL.Emit(OpCodes.Ret);

Agora, como você bem sabe, assim que define um construtor personalizado para um tipo, o construtor padrão é removido silenciosamente. Para redefinir o construtor sem argumento, basta chamar o método DefineDefaultConstructor () do tipo TypeBuilder da seguinte maneira:

// Create the default ctor.
helloWorldClass.DefineDefaultConstructor(MethodAttributes.Public);

Essa chamada única emite o código CIL padrão usado para definir um construtor padrão.
 

Emitindo o método SayHello ()

Por último, mas não menos importante, vamos examinar o processo de emissão do método SayHello (). A primeira tarefa é obter um tipo MethodBuilder da variável helloWorldClass. Depois de fazer isso, defina o método e obtenha o ILGenerator subjacente para injetar as instruções CIL, da seguinte maneira:

// Now create the GetMsg() method.
MethodBuilder getMsgMethod = helloWorldClass.DefineMethod("GetMsg",
    MethodAttributes.Public, typeof(string), null);

ILGenerator methodIL = getMsgMethod.GetILGenerator();
methodIL.Emit(OpCodes.Ldarg_0);
methodIL.Emit(OpCodes.Ldfld, msgField);
methodIL.Emit(OpCodes.Ret);

Aqui você estabeleceu um método público (MethodAttributes.Public) que não aceita parâmetros e não retorna nada (marcado pelas entradas nulas contidas na chamada DefineMethod ()). Observe também a chamada EmitWriteLine (). Esse membro auxiliar da classe ILGenerator grava automaticamente uma linha na saída padrão com o mínimo de esforço e incomodo.

Usando o assembly gerado dinamicamente

Agora que você tem a lógica para criar e salvar sua montagem, tudo o que é necessário é uma classe para acionar a lógica. Para dar um círculo completo, suponha que seu projeto atual defina uma segunda classe chamada AsmReader. A lógica em Main () obtém o AppDomain atual por meio do método Thread.GetDomain () que será usado para hospedar o assembly que você criará dinamicamente. Depois de ter uma referência, você poderá chamar o método CreateMyAsm ().

Para tornar as coisas um pouco mais interessantes, depois que a chamada para CreateMyAsm () retornar, você exercitará alguma ligação tardia para carregar seu assembly recém-criado na memória e interagir com os membros da classe HelloWorld. Atualize seu método Main () da seguinte maneira:

static void Main(string[] args)
{
    Console.WriteLine("***** The Amazing Dynamic Assembly Builder App *****");
    // Get the application domain for the current thread.
    AppDomain curAppDomain = Thread.GetDomain();
    // Create the dynamic assembly using our helper f(x).
    CreateMyAsm(curAppDomain);
    Console.WriteLine("-> Finished creating MyAssembly.dll.");
    // Now load the new assembly from file.
    Console.WriteLine("-> Loading MyAssembly.dll from file.");
    Assembly a = Assembly.Load("MyAssembly");
    // Get the HelloWorld type.
    Type hello = a.GetType("MyAssembly.HelloWorld");
    // Create HelloWorld object and call the correct ctor.
    Console.Write("-> Enter message to pass HelloWorld class: ");
}

Você acabou de criar um assembly .NET capaz de criar e executar assemblies .NET em tempo de execução! Observe também que o método CreateMyAsm() usa como único parâmetro um tipo System.AppDomain, que será usado para obter acesso ao tipo AssemblyBuilder associado ao domínio de aplicativo atual. A seguir, o código completo a seguir:

public static void CreateMyAsm(AppDomain curAppDomain)
{
    // Establish general assembly characteristics.
    AssemblyName assemblyName = new AssemblyName();
    assemblyName.Name = "MyAssembly";
    assemblyName.Version = new Version("1.0.0.0");
    // Create new assembly within the current AppDomain.
    // AssemblyBuilder assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,
    AssemblyBuilder assembly = curAppDomain.DefineDynamicAssembly(assemblyName,
        AssemblyBuilderAccess.Save);

    // Given that we are building a single-file
    // assembly, the name of the module is the same as the assembly.
    ModuleBuilder module = assembly.DefineDynamicModule("MyAssembly", "MyAssembly.dll");

    // Define a public class named "HelloWorld".
    TypeBuilder helloWorldClass = module.DefineType("MyAssembly.HelloWorld",
        TypeAttributes.Public);

    // Define a private String member variable named "theMessage".
    FieldBuilder msgField = helloWorldClass.DefineField("theMessage",
        Type.GetType("System.String"), FieldAttributes.Private);

    // Create the custom ctor.
    Type[] constructorArgs = new Type[1];
    constructorArgs[0] = typeof(string);
    ConstructorBuilder constructor = helloWorldClass.DefineConstructor(MethodAttributes.Public,
    CallingConventions.Standard, constructorArgs);

    ILGenerator constructorIL = constructor.GetILGenerator();
    constructorIL.Emit(OpCodes.Ldarg_0);

    Type objectClass = typeof(object);
    ConstructorInfo superConstructor = objectClass.GetConstructor(new Type[0]);
    constructorIL.Emit(OpCodes.Call, superConstructor);
    constructorIL.Emit(OpCodes.Ldarg_0);
    constructorIL.Emit(OpCodes.Ldarg_1);
    constructorIL.Emit(OpCodes.Stfld, msgField);
    constructorIL.Emit(OpCodes.Ret);

    // Create the default ctor.
    helloWorldClass.DefineDefaultConstructor(MethodAttributes.Public);

    // Now create the GetMsg() method.
    MethodBuilder getMsgMethod = helloWorldClass.DefineMethod("GetMsg",
        MethodAttributes.Public, typeof(string), null);

    ILGenerator methodIL = getMsgMethod.GetILGenerator();
    methodIL.Emit(OpCodes.Ldarg_0);
    methodIL.Emit(OpCodes.Ldfld, msgField);
    methodIL.Emit(OpCodes.Ret);

    // Create the SayHello method.
    MethodBuilder sayHiMethod = helloWorldClass.DefineMethod("SayHello",
    MethodAttributes.Public, null, null);

    methodIL = sayHiMethod.GetILGenerator();
    methodIL.EmitWriteLine("Hello from the HelloWorld class!");
    methodIL.Emit(OpCodes.Ret);

    // "Bake" the class HelloWorld.
    // (Baking is the formal term for emitting the type.)
    helloWorldClass.CreateType();
    // (Optionally) save the assembly to file.
    assembly.Save("MyAssembly.dll");
}

Assembly estáticos

Assemblies estáticos são armazenados em disco em arquivos PE. Os assemblies estáticos podem incluir interfaces, classes e recursos como bitmaps, arquivos JPEG e outros arquivos de recurso. Em geral, um assembly estático pode consistir em quatro elementos:
•	Assembly Manifest, que contém metadados do assembly. Fornece informações ao CLR, como o nome, versão e outros assemblies a que ele faz referência
•	Application Manifest, que contém metadados de tipo. Fornece informações ao sistema operacional, como como o conjunto deve ser implantado e se a elevação administrativa é necessária
•	Tipos compilados, código CIL (Commom Intermediate Language) e os metadados dos tipos definidos no assembly. Ele é gerado pelo compilador a partir de um ou mais arquivos de código fonte.
•	Conjunto de recursos: Outros dados incorporados na montagem, como imagens e texto localizável

Somente o Assembly Manifest é obrigatório, mas tipos e recursos são necessários para atribuir ao assembly uma funcionalidade significativa. Um Assembly Manifest não é algo que você adiciona explicitamente a uma montagem - é automaticamente incorporado a uma montagem como parte da compilação. 

Assembly Manifest

Todo assembly, estático ou dinâmico, contém uma coleção de dados que descrevem como os elementos em um assembly se relacionam uns com os outros. O assembly manifest contém todo o metadata necessário para o assembly definir versão, identificar aspectos relativos à segurança e referências para recursos e classes. O manifest pode ser armazenado junto com o CIL no PE (.dll ou .exe) ou em um PE separado
1.	Uma estrutura de dados que armazena informação sobre um assembly. 
2.	Esta informação é armazenada dentro do próprio arquivo assembly(DLL/EXE)
3.	A informação descreve como os elementos do assembly se relacionam e inclui : lista de arquivos constituintes , informação sobre a versão , nome do assembly, etc.
4.	Este conjunto de informações é conhecido como metadata ou metadados.

O metadata descreve tipos e membros contidos em uma aplicação. Quando convertemos o código C# em um Portable Executable (PE), o metadata é inserido em uma porção desse arquivo, enquanto o código é convertido para CIL e inserido em outra porção desse mesmo arquivo. Quando o código é executado, o metadata é carregado na memória, juntamente com as referências para as classes, os membros, a herança etc. Aqui está um resumo dos dados funcionalmente significativos armazenados no manifesto:
•	O nome simples da montagem
•	Um número de versão (AssemblyVersion)
•	Uma chave pública e um hash assinado do assembly, se fortemente nomeado
•	Uma lista de assemblies referenciados, incluindo sua versão e chave pública
•	Uma lista de tipos definidos na montagem
•	A cultura que ele segmenta, se um assembly satélite (AssemblyCulture)
•	Permissões de segurança necessárias para a execução.
•	Descrição de tipos.
•	Nome, visibilidade, classe base e interfaces implementadas.
•	Membros (métodos, campos, propriedades, eventos etc.).
•	Atributos.
•	Elementos descritivos adicionais que modificam tipos e membros.
•	Uma tabela de arquivos que descreve todos os outros arquivos que compõem o assembly, como outros assemblies que você criou em que o arquivo . exe ou . dll depende, arquivos de bitmap ou arquivos Leiame.
•	Uma lista de referências de assembly, que é uma lista de todas as dependências externas, como . dlls ou outros arquivos. As referências de assembly contêm referências a objetos globais e privados. Os objetos globais estão disponíveis para todos os outros aplicativos. No .NET Core, os objetos globais são acoplados a um tempo de execução específico do .NET Core. No .NET Framework, os objetos globais residem no GAC (cache de assembly global). System. IO. dll é um exemplo de um assembly no GAC. Os objetos privados devem estar em um nível de diretório no ou abaixo do diretório no qual seu aplicativo está instalado.

O manifesto também pode armazenar os seguintes dados informativos:
•	Um título e uma descrição completos (AssemblyTitle e AssemblyDescription)
•	Informações sobre empresa e direitos autorais (AssemblyCompany and Assembly Copyright)
•	Uma versão de exibição (AssemblyInformationalVersion)
•	Atributos adicionais para dados customizados

Alguns desses dados são derivados de argumentos fornecidos ao compilador, como a lista de assemblies referenciados ou a chave pública com a qual assinar o assembly. O restante é proveniente de atributos de montagem, indicados entre parênteses. Você pode visualizar o conteúdo do manifesto de um assembly com a ferramenta .NET ildasm.exe.

Como os assemblies contêm informações sobre conteúdo, controle de versão e dependências, os aplicativos que os usam não precisam depender de fontes externas, como o registro em sistemas Windows, para funcionar corretamente. Assemblies são, portanto, auto-descritivas. Um consumidor pode descobrir todos os dados, tipos e funções de uma montagem, sem precisar de arquivos adicionais.

Os assemblies reduzem conflitos .dll e tornam seus aplicativos mais confiáveis e fáceis de implantar. Em muitos casos, você pode instalar um aplicativo baseado em .NET simplesmente copiando seus arquivos para o computador de destino. 

Quando queremos usar bibliotecas de terceiros, importamos assemblies (dlls) em nosso projeto, que geralmente ficam na pasta references. Se as classes dentro do assembly que você for utilizar em seu projeto estiverem com a visibilidade internal, você simplesmente não conseguirá usá-las. Use internal somente quando não for compartilhar esse código com nenhum outro assembly. 

Ou seja, quando não for usar o código de um projeto em outro: geralmente classes utilitárias que você só usará internamente. Quando criamos uma classe no C# sem definir um modificador de acesso(geralmente public): 
class NomeDaClasse  
{
}

Application Manifest (Windows)

Um manifesto de aplicativo é um arquivo XML que comunica informações sobre o assembly ao sistema operacional. Um manifesto de aplicativo é incorporado ao executável de inicialização como um recurso Win32 durante o processo de compilação. Se presente, o manifesto é lido e processado antes que o CLR carregue o assembly - e pode influenciar como o Windows inicia o processo do aplicativo.

O manifesto do aplicativo contém informações sobre o aplicativo, como permissões necessárias, assemblies a serem incluídos e outras dependências.

Um manifesto de aplicativo .NET possui um elemento raiz chamado assembly no namespace XML urn: schemas-microsoft-com: asm.v1:

<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoftcom:asm.v1">
  <!-- contents of manifest -->
</assembly>

O manifesto a seguir instrui o sistema operacional a solicitar elevação administrativa:

<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoftcom:asm.v1">
  <trustInfo xmlns="urn:schemas-microsoft-com:asm.v2">
    <security>
      <requestedPrivileges>
        <requestedExecutionLevel level="requireAdministrator" />
      </requestedPrivileges>
    </security>
  </trustInfo>
</assembly>

Os aplicativos UWP têm um manifesto muito mais elaborado, descrito no arquivo Package.appxmanifest. Isso inclui uma declaração dos recursos do programa, que determinam as permissões concedidas pelo sistema operacional. A maneira mais fácil de editar esse arquivo é com o Visual Studio, que exibe uma caixa de diálogo quando você clica duas vezes no arquivo de manifesto.

Você pode adicionar um manifesto de aplicativo a um projeto do .NET Core no Visual Studio clicando com o botão direito do mouse em seu projeto no Solution Explorer, selecionando Add, depois em “New item” e, em seguida, escolhendo Application Manifest File. Na construção, o manifesto será incorporado ao conjunto de saída.

Conjunto de Recursos

Há duas maneiras de utilizarmos recursos tais como strings, imagens e arquivos texto em aplicações .NET Framework: podemos embuti-las diretamente na aplicação ou carregá-las de um arquivo externo. Quando escolhemos carregá-las de uma fonte externa em lugar de um recurso embutido, devemos distribuir os arquivos junto com o assembly. Devemos também verificar que o código da aplicação poderá determinar o caminho correto e carregar os arquivos de recursos em tempo de execução. Esta abordagem causará problemas caso o .exe estiver separado dos arquivos dos quais depende.

Adotando a opção embutida e compilando os recursos necessários diretamente nos assemblys que os utilizam, tornamos a distribuição mais confiável e menos sujeita a erros. A possibilidade de incluir arquivos dentro de um Assembly facilita a distribuição, já que você não precisa se preocupar em manter os caminhos dos arquivos, já que eles estão embutidos e a forma de acessá-los é diferente. Além disso, temos uma maior segurança, pois depois de compilado, não é mais possível alterá-lo a menos que, abra o projeto e edite o arquivo e, finalmente, gere um novo Assembly contendo as novas informações.

Geralmente essas recurso embutidos são imagens, arquivos xml, arquivos texto, mensagens,etc.. Se analisarmos o próprio .NET Framework, ele possui uma porção de recursos e, como exemplo, podemos citar o Assembly System.Windows.Forms.dll que embuti várias imagens que são utilizadas pelos controles que lá estão contidos quando são colocados na barras de ferramentas do Visual Studio .NET. Um outro exemplo é o caso do Assembly System.Web.dll que, na seção de recursos, além de imagens, possui também arquivos javascript que são utilizados pelos controles do ASP.NET.

Suponhamos que desejemos embutir na aplicação, uma imagem gráfica chamada Background.png. Começamos criando um projeto biblioteca de classes Visual Studio. 

Abra o Visual Studio>>Selecione Visual C #>>Selecione a Biblioteca de Classes >>Digite o nome do projeto
 

Você pode ver claramente que o projeto da biblioteca de classes não possui um método principalMain. Portanto, uma biblioteca de classes não tem um ponto de entrada. Vamos criar um método simples dentro da Class1, que retorna o quadrado de um número inteiro.Observe que um membro da biblioteca de classes deve ser público, para que um membro possa ser acessado em outro projeto.

namespace ClassLibrary_Recurso
{
publicclassClass1
    {
publicstaticint square(int i)
        {
return (i * i);
        }
    }
}

Inclua um arquivo de imagem no projeto e, a seguir, na janela de propriedades do arquivo, configure a Build Action para Embedded Resource, como mostrado na Figura. Fazendo assim, instruimos o Visual Studio a embutir o arquivo na imagem física do arquivo .exe do assembly de saída.
 

Pressione Ctrl + Shift + B para criar oassembly. Agora, vá para a pasta Bin >> Debug do projeto; lá você verá um arquivo MyCustomLibrary.dll. Parabéns, você criou seu primeiro assembly .DLL com recurso embutido.
 
Uma vez que embutimos um arquivo como um recurso, veremos como acessá-lo em tempo de execução através de uma aplicação Windows Forms. Vamos examinar o seguinte fragmento de código, que obtém uma referência para o objeto assembly criado ClassLibrary_Recurso. 
 
A seguir chama o método GetManifestResourceStream para ganhar acesso stream ao arquivo de recursos embutido. O trecho de código abaixo exibe a forma de recuperar a imagem via código e definí-la como sendo o Background. Se aimagem gráfica é chamada Background.png, a chave para acessá-la será NomeProjeto.Background.jpg. Este código supõe que importamos os namespaces System.Reflection e System.Drawing:

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Load_Recurso_Forms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Assembly assembly = Assembly.LoadFrom("ClassLibrary_Recurso.dll");
            string[] names = assembly.GetManifestResourceNames();

            var oform = new Form1();
            var image = assembly
                .GetManifestResourceStream("ClassLibrary_Recurso.Background.png");

            //Image img = Image.FromStream(image);
            //PictureBox1.Image = img;

            int cont = 4;
            int sqri = ClassLibrary_Recurso.Class1.square(cont);
            MessageBox.Show("Square of " + cont + " = " + sqri);

            oform.BackgroundImage = new Bitmap(image);
            Application.Run(oform);
        }
    }
}

Como podemos ver, um objeto assembly expõe o método GetManifestResourceStream, que permite que passemos um nome string que identifica o recurso embutido. Notar que o nome de recursos é case-sensitive mesmo quando estivermos utilizando uma linguagem não case-sensitive. 
	
Caso seja necessário convert o stream para imagem, o método Image.FromStream converte o stream que contém o arquivo imagem em um objeto Image que pode ser carregado em um controle PictureBox.

Image img = Image.FromStream(image);
PictureBox1.Image = img;

Além de embutir arquivos de imagem, pode ser conveniente embutir arquivos texto que contenham XML, SQL ou JavaScript. Isto tornar nossa vida muito mais fácil, quando formos concatenar grandes fragmentos string XML, SQL ou JavaScript, utilizando o Visual Basic.

Por exemplo, digamos que a aplicação necessita grandes documentos XML, declarações SQL, ou funções JavaScript. Poderíamos mantê-los como arquivos .xml, .sql e .js independentes em um projeto Visual Studio. Com isto obtemos os benefícios da codificação colorida e da conclusão de declarações (statment completion) do Visual Studio. Podemos também tirar vantagem do IntelliSense do Visual Studio para arquivos XML. Apenas precisamos embutir estes arquivos fonte no assembly de saída e acessá-los utilizando a técnica já descrita.

AssemblyInfo.cs

Sempre que um novo projeto de montagem .NET é criado no Visual Studio, é criado um arquivo chamado AssemblyInfo.Este arquivo é criado por padrão nas maiorias das aplicações e é através dele que podemos colocar várias informações a nível de Assembly. Através de vários atributos fornecidos pelo .NET Framework, podemos definir várias informações como por exemplo: nome do produto, versão, empresa, descrição, cultura, segurança, etc.. Quando você cria uma aplicação utilizando o Visual Studio .NET, esse arquivo já é gerado automaticamente e você pode abrí-lo e configurar de acordo com a sua necessidade.

O arquivo AssemblyInfo.cs pode ser encontrado no Solution Explorer>>Propriedades>>AssemblyInfo.cs"
 

Versionamento

Como um assembly é a unidade de implantação de qualquer aplicativo .NET, se você deseja dividir seu aplicativo em unidades menores de implantação para torná-lo mais gerenciável e reutilizável por diferentes produtos, é necessário dividir a funcionalidade em diferentes assemblies. Cada montagem pode representar novamente um módulo em seu aplicativo, que pode ser atualizado independentemente dos outros projetos. A maneira mais simples de implementar esse tipo de modularização é criar projetos diferentes, onde um projeto é um módulo, e agrupá-los em uma grande solução. Para diferenciar versões diferentes da mesma montagem, você precisa usar as versões.

O software evolui e a maneira de marcar isso é criando novas versões, com cada nova versão adicionando novos recursos, corrigindo bugs ou, às vezes, reescrenvdo completamente o software. Você pode ver dois tipos principais de versões: 
•	Versão da Montagem: É um atributo implementado no AssemblyVersionAttribute aplicado ao assembly que especifica qual a versão do assembly. Este atributo é ignorado pelo CLR, a menos que seja usado junto com os nomes fortes.  AssemblyVersionAttribute deve ser incrementado manualmente. Isso deve ser feito quando você planeja implantar uma versão específica na produção.
•	Versão do Arquivo. É um atributo implementado no AssemblyFileVersionAttribute correspondente para a versão do arquivo e é algo que tem um significado para você ou seu produto. O CLR não usa a versão do arquivo, normalmente apenas o departamento de marketing se preocupa com a versão do arquivo.
Embora a versão do arquivo seja normalmente especificada no mesmo formato que a versão da montagem com um número principal, secundário, de construção e de revisão, você pode ter qualquer sequência que desejar. Porém, se você usar um formato diferente, o compilador emitirá um aviso. Você não pode definir a versão do arquivo como uma string na janela Informações da montagem. Você deve fazer isso no código, passando o valor para o atributo AssemblyFileVersionAttribute.

A versão atual do .NET é conhecida como 4.5 que é o número da versão de marketing que os clientes podem ver. A versão do assembly .NET é implementada como uma sequência, normalmente composta de quatro partes numéricas separadas por pontos. Por exemplo a versão de montagem .NET 4.5 seria representada por:
4.0.30319.17929,
onde:
[assembly: AssemblyVersion(“{Major}.{Minor}.{Build Number}.{Revision}”)]
1.	Maior ou principal: 4 representa o número principal. Aumenta sempre que você tem uma versão principal do seu produto, alterando os recursos existentes ou reescrevendo o aplicativo inteiro.
2.	Menor ou secundário: 0 é o número secundário. É aumentado a cada versão pública do seu produto. Normalmente, duas versões do mesmo conjunto com o mesmo número principal, mas números menores diferentes são compatíveis com versões anteriores - embora esse nem sempre seja o caso. Compatibilidade com versões anteriores significa que, ao substituir uma montagem por uma versão mais recente da mesma, o sistema continuará funcionando. A maneira de conseguir isso é adicionando novos recursos, mas nunca removendo os recursos existentes ou alterando a face pública dos existentes.
3.	Número da compilação/construção: 30319 é o número da compilação. É um número que normalmente aumenta todos os dias, enquanto você trabalha em uma determinada versão do seu produto. Isso é feito normalmente todas as noites pelo seu processo de compilação noturno.
4.	Revisão: 17929 é a revisão. É um número aleatório usado para diferenciar duas versões que têm o mesmo número de compilação. Normalmente, você deve definir um novo número toda vez que fizer o check-in e seu projeto for criado.

Escolha o esquema de versão com cuidado, pois isso pode ajudá-lo a longo prazo. Se você usar a maneira recomendada com números é muito mais fácil para dar um significado à versão da montagem, ou seja, é mais fácil identificar qual versão é mais nova com base no número da versão. Se você optar por ter um número de versão que esteja em outro formato, o compilador gerará um erro. 

 Você pode definir a versão da sua montagem de duas maneiras. O primeiro é através das configurações do seu projeto. Clique com o botão direito do mouse nas configurações do seu projeto e escolha Propriedades. Na página de propriedades Aplicativo, pressione o botão Informações da Montagem.
 

Uma caixa de diálogo é exibida.Os 4 campos da versão de montagem representam as partes mencionadas.
 

A segunda maneira de definir a versão é ir diretamente e editar o arquivo AssemblyInfo.cs. Para encontrar o arquivo, você deve clicar no projeto para o qual deseja alterar a versão e, em seguida, expandir o nó de propriedade. Se você criar um novo projeto no Visual Studio, no final do arquivo AssemblyInfo.cs, você pode encontrar duas linhas semelhante ao seguinte código:

[assembly: AssemblyVersion ("1.0.0.0")]
[assembly: AssemblyFileVersion ("1.0.0.0")]
 

O Net Framework oferece também AssemblyInformationalVersionAttribute, um terceiro atributo para controle de versão. Este é um texto sem formatação e normalmente é o valor da sua versão do produto. Se você não o configurar, ele terá o valor de AssemblyVersionAttribute. 

Para ver os valores de AssemblyFileVersionAttribute e AssemblyInformationalVersionAttribute, você pode compilar sua montagem, usando o Windows Explorer, navegue até a pasta de saída e clique com o botão direito do mouse na montagem. Em seguida, escolha o menu Propriedade para abrir a janela de propriedades do arquivo e escolha a guia Detalhes. Um diálogo semelhante a figura abaixo deve aparecer.
 

O valor do AssemblyFileVersionAttribute é representado pelo valor da propriedade Versão do Arquivo e pelo valor deAssemblyInformationalVersionAttribute é representado pelo valor da propriedade Product Version.

O processo de localização da montagem correta começa com o número da versão mencionado no arquivo de manifesto da montagem original para determinar qual montagem carregar. Essas ligações podem ser influenciadas com arquivos de configuração específicos, no entanto. Três arquivos de configuração são usados:
•	Arquivos de configuração do aplicativo
•	Arquivos de política do editor
•	Arquivos de configuração da máquina

Esses arquivos de configuração podem ser usados para influenciar a ligação de assemblies referenciados. Suponha, por exemplo, que você implantou um assembly no GAC e e alguns aplicativos dependem dele. De repente, um bug é descoberto e você cria uma correção para ele. A nova montagem possui um novo número de versão e você deseja garantir que todos os aplicativos usem a nova montagem.

Você pode fazer isso usando no “publisher policy file”. Nesse arquivo de configuração, você especifica que, se o CLR procurar um assembly específico, ele deverá se vincular à nova versão. O exemplo abaixo mostra um exemplo de como esse arquivo ficaria.

<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="myAssembly"
        publicKeyToken="32ab4ba45e0a69a1"
        culture="en-us" />
        <!-- Redirecting to version 2.0.0.0 of the assembly. -->
        <bindingRedirect oldVersion="1.0.0.0"
                         newVersion="2.0.0.0"/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>

Este arquivo instrui o CLR a vincular-se à versão 2 do assembly em vez da versão 1. Você precisa implantar uma política de editor no GAC para que o CLR possa usá-lo ao vincular assemblies.


Se você tiver um assembly implantado em particular com seu aplicativo, o CLR começará a procurá-lo no diretório de aplicativos atual. Se não conseguir encontrar o assembly, ele lançará uma FileNotFoundException. Você pode,  no arquivo de configuração do aplicativo, especificar locais extras onde o CLR deve procurar um diretório que são relativos ao caminho do aplicativo Você usa a seção probing (análise) para isso, como mostra abaixo. 

<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <!-- Especificar locais extras onde o CLR deve procurar fora do diretório de aplicativos  -->
      <probing privatePath="MyLibraries"/>
    </assemblyBinding>
  </runtime>
</configuration>

Outra opção é usar o elemento codebase. Um elemento codebase pode especificar um local para um assembly que está fora do diretório do aplicativo. Dessa forma, você pode localizar uma montagem em outro computador na rede ou em algum lugar da Internet. Esses assemblies devem ter nomes fortes se não estiverem na pasta do aplicativo atual. Quando a montagem está localizada em outro computador, é baixada para uma pasta especial no GAC. O exemplo abaixo mostra um exemplo de uso do elemento codebase para especificar o local de um montagem em algum lugar na web.

<configuration>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
      <!-- Especificar locais extras onde o CLR deve procurar fora do diretório de aplicativos  -->
        <codeBase version="1.0.0.0"
        href= "http://www.mydomain.com/ReferencedAssembly.dll"/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>

Assinando montagens usando um nome forte (Strong Names)

O CLR suporta dois tipos diferentes de montagens: montagens com nome forte e montagens regulares. Uma montagem regular é o que o Visual Studio gera para você por padrão. É estruturalmente idêntico a uma montagem de nome forte. Ambos contêm metadados, cabeçalho, manifesto e todos os tipos que estão no seu assembly.

Para resolver o problema do “DLL Hell” o .NET introduziu com os assemblies um novo conceito chamado versionamento lado a lado, que nada mais é do que fazer uma distinção entre diferentes versões do mesmo assembly, assegurando que as duas versões diferentes sejam provenientes da mesma fonte. Como o nome, cultura e a versão do Assembly podem repetir de uma empresa para outra, a Microsoft decidiu utilizar tecnologias de criptografias baseadas em chave pública/privada para garantir a unicidade do Assembly. As informações que compõem um Assembly que tem uma uma assinatura digital por uma strong name consiste em quatro atributos, que o identificarão unicamente: 
•	Nome do arquivo (sem extensão): Na situação normal, esse é o nome do arquivo de montagem sem a extensão, portanto, se o arquivo de montagem foi chamado ProgrammingCSharp.dll, o nome amigável para montagem seria ProgrammingCSharp.
•	Número de versão: A versão é a versão da montagem discutida na seção anterior.
•	Informações de cultura: A cultura representa a cultura da montagem. A cultura é usada quando você deseja localizar seu aplicativo para diferentes mercados. Para localizar seu aplicativo, você cria um assembly contendo o código e um assembly para cada região para a qual deseja que o aplicativo seja localizado. As montagens localizadas contêm apenas recursos para essa região específica, como cadeias traduzidas ou imagens específicas. O assembly executável sempre tem a cultura definida como neutra. Se você tentar configurá-lo para outra coisa, um erro será gerado pelo complier.
•	O token da chave pública: A chave privada é usada para assinar digitalmente o assembly, enquanto a chave pública é usada para verificar a assinatura. O Token de Chave Pública é um hash de 64 bits da chave pública do assembly. O motivo pelo qual o .NET usa um hash da assinatura em vez da própria assinatura é economizar espaço, porque a assinatura é muito maior que 64 bits. Um assembly que não é assinado digitalmente pode ter o Public Key Token definido como null. 

Nomear fortemente um assembly tem vários benefícios:
•	Garantem exclusividade. Sua chave privada exclusiva é usada para gerar o nome para seu assembly. Nenhuma outra montagem pode ter exatamente o mesmo nome forte.
•	Protegem sua linhagem de versão. Como você controla a chave privada, você é o único que pode distribuir atualizações para seus assemblies. Os usuários podem ter certeza de que a nova versão se origina do mesmo editor.
•	Fornecem uma forte verificação de integridade. O .NET Framework verifica se um assembly de nome forte foi alterado desde o momento em que foi assinado.

No geral, você pode ver que um assembly de nome forte garante ao usuário que ele pode confiar na origem e no conteúdo de um assembly. Você gera um assembly de nome forte usando sua própria chave privada para assinar o assembly. Outros usuários podem verificar o assembly usando a chave pública que é distribuída com o assembly.

Para assinar um assembly com um nome forte, o primeiro passo que você deve executar é gerar um par de chaves pública/privada. Esse par de chaves de criptografia pública/privada é usado durante a compilação para criar um assembly de nome forte. Você pode gerar essas chaves de duas maneiras. 
•	Criar o arquivo via Visual Studio (extensão pfx): O certificado é protegido por uma senha. Pode causar o problema de que você precisa digitar essa senha quando irá executar o código fonte pela primeira vez em uma nova máquina. Isso não é conveniente ao fazer a compilação automatizada, quando nenhum ser humano digita uma senha. Isso nos dá essa mensagem de erro.

Cannot import the following key file: cert.pfx. The key file may be password protected. To correct this, try to import the certificate again or manually install the certificate to the Strong Name CSP with the following key container name: VS_KEY_XXXXXXXXXXXXXXX

O cenário é ainda pior quando você tenta configurá-lo com as máquinas hospedadas na compilação. Essas máquinas são pré-configuradas pela Microsoft para evitar a necessidade de configurar sua própria máquina de compilação. (você não pode acessá-lo).
•	Pelo utilitário sn.exe (extensão snk): a desvantagem de usar o sn.exe é que você não pode especificar a senha, mas o sn.exe é usado no cenário de atraso de sinal, para extrair a chave pública do arquivo ou para assinar o assembly antes da implantação. O atraso na assinatura não é necessário para o exame, mas você pode pesquisar mais sobre este assunto


Nos últimos 15 anos, os fabricantes de CPU passaram de processadores de singlecore para multicore. Isso é problemático para nós, como programadores, porque o código de thread único não é executado automaticamente mais rápido como resultado desses núcleos extras.

A utilização de vários núcleos é fácil para a maioria dos aplicativos de servidor, em que cada thread pode manipular independentemente uma solicitação de cliente separada, mas é mais difícil na área de trabalho, porque geralmente requer que você use seu código intensivo em computação e faça o seguinte:
1. Particione em pequenos pedaços.
2. Execute esses pedaços em paralelo via multithreading.
3. Agrupe os resultados assim que estiverem disponíveis, de maneira segura e com desempenho eficiente.

Embora você possa fazer tudo isso com as construções clássicas de multithreading, é estranho, principalmente as etapas de particionamento e agrupamento. Um problema adicional é que a estratégia usual de bloqueio para segurança de encadeamento causa muita contenção quando muitos encadeamentos trabalham nos mesmos dados ao mesmo tempo.

Criar chaves via Visual Studio

Para fazer isso, clique com o botão direito do mouse no projeto e escolha Propriedades. Na página de propriedades, escolha a guia Assinatura. Lá, marque a caixa de seleção “Assinar a montagem”, como mostra a Figura abaixo.
 

Como você pode ver, é possível especificar o nome do arquivo de chave, uma senha que você pode usar para proteger a chave privada no arquivo e o algoritmo a ser usado para a assinatura. As opções para o algoritmo são sha256RSA e sha1RSA (esse algoritmo usado em certificados antigos). Depois de criar o arquivo, ele será adicionado ao seu projeto. Quando seu projeto é aberto por outro usuário ou por você em outra máquina, é necessário introduzir a senha que você acabou de definir na etapa anterior.
 

É recomendável que você use uma senha forte para o arquivo-chave e disponibilize a senha apenas para alguns desenvolvedores selecionados - ou melhor ainda, para a pessoa responsável pela implantação de seus aplicativos. Se a senha for comprometida, será fácil para outra pessoa criar assemblies e distribuí-los como se fossem de você. Para manter a senha em segredo, mas ainda ter outros desenvolvedores trabalhando com seu aplicativo, você deve usar apenas “Somente assinatura atrasada”. 

Criar chaves via sn.exe

A Microsoft disponibilizou um utilitário de linha de comando que permite-nos gerar uma strong name para assinarmos os Assemblys que desejarmos. Esse utilitário chama-se sn.exe (sn = strong name) e, para gerar um par de chave pública/privada, basta simplesmente fazer:
1)	Encontrar, prompt de comando do Visual Studio no StartMenu, execute-o como administrador.
 

2)	Navege para a pasta raiz do projeto da biblioteca de classes, cd “{PathLocation}”, pressione enter.  Se quiser, poderá abrir o Visual Studio 2005 Command Prompt para poupar a digitação do caminho completo até o utilitário.
3)	crie uma chave de nome forte escrevendo um commando “sn -k {KeyName}.snk”. O parâmetro –k indica ao utilitário para gerar a chave. Por exemplo, “sn -k myClassLibrarykey.snk” e pressione Enter.

O arquivo gerado através do comando acima conterá as chaves pública e privada.A saída do comando deve ser semelhante à da Figura
 
 
Um assembly de nome forte pode fazer referência apenas a outros assemblies que também são fortemente nomeados. Isso evita falhas de segurança nas quais um assembly dependente pode ser alterado para influenciar o comportamento de um assembly de nome forte. Quando você adiciona uma referência a um assembly regular e tenta invocar o código desse assembly, o compilador emite um erro:

Assembly generation failed -- Referenced assembly ‘MyLib’ does not have a strong name,.

Depois de assinar um assembly, você pode visualizar a chave pública usando a ferramenta Nome Forte (Sn.exe) instalada no Visual Studio. 
sn -Tp StrongNameLib.dll
 

Um dos assemblies fortemente nomeados que são instalados com o .NET Framework é System.Data. O exemplo abaixo mostra como você pode obter a chave pública deste assembly:
sn -Tp C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Data.dll
 

O token de chave pública é uma pequena sequência que representa a chave pública. É gerado com hash da chave pública e com os últimos oito bytes. Se você referenciar outro assembly, você armazenará apenas o token de chave pública, que preserva espaço no manifesto do assembly. O CLR não usa o token de chave pública ao tomar decisões de segurança porque pode acontecer que várias chaves públicas tenham o mesmo token de chave pública.
Dentro de uma organização, é importante proteger a chave privada. Se todos os funcionários tiverem acesso à chave privada, alguém poderá vazar ou roubar a chave. Eles podem distribuir assemblies que parecem legítimos. Mas, sem acesso à chave privada, os desenvolvedores não podem assinar o assembly e usá-lo durante a criação do aplicativo.
Para evitar esse problema, você pode usar um recurso chamado assinatura atrasada (delayed) ou parcial (partial). Ao usar a assinatura atrasada, você usa apenas a chave pública para assinar uma montagem e a chave privada até o projeto estar pronto para implantação. 
Authenticode 

Autenticação no Authenticode 


Authenticode é um sistema de assinatura de código cujo objetivo é provar a identidade do editor.  Authenticode e assinatura com nome forte são independentes: é possível assinar um assembly com um ou ambos os sistemas.  Embora a assinatura com nome forte possa provar que os assemblies A, B e C vieram da mesma publicador (supondo que a chave privada não tenha sido vazada), ela não pode dizer quem foi essa parte.  Para saber que o publicador era Joe Albahari - ou Microsoft Corporation - você precisa do Authenticode.  

O Authenticode é útil ao baixar programas da Internet, porque garante que um programa veio de quem foi nomeado pela Autoridade de Certificação e não foi modificado em trânsito.  Também evita o aviso de Publicador Desconhecido (Unknown Publisher) ao executar um aplicativo baixado pela primeira vez.  A assinatura Authenticode também é um requisito ao enviar aplicativos para a Windows Store.  

O Authenticode funciona não apenas com assemblies .NET, mas também com executáveis e binários não gerenciados, como arquivos de implantação .msi.  Obviamente, o Authenticode não garante que um programa esteja livre de malware, embora o torne menos provável.  O CLR não trata uma assinatura Authenticode como parte da identidade de uma montagem.  No entanto, ele pode ler e validar assinaturas Authenticode sob demanda.

A assinatura com o Authenticode exige que você entre em contato com uma Autoridade de Certificação (CA) com evidência de sua identidade pessoal ou da empresa (artigos de incorporação etc.).  Depois que a CA verificar seus documentos, ela emitirá um certificado de assinatura de código X.509 que normalmente é válido por um a cinco anos.  Isso permite que você assine montagens com o utilitário signtool.  Você também pode fazer um certificado com o utilitário makecert;  no entanto, ele será reconhecido apenas em computadores nos quais o certificado está instalado explicitamente.  O fato de que os certificados (não autoassinados) podem funcionar em qualquer computador depende da infraestrutura de chave pública. Essencialmente, seu certificado é assinado com outro certificado pertencente a uma CA.  A CA é confiável porque todas as CAs são carregadas no sistema operacional (para vê-las, vá para o Painel de Controle do Windows e, na caixa de pesquisa, digite "certificado". Na seção Ferramentas Administrativas, clique em "Gerenciar certificados do computador".  inicia o Gerenciador de certificados, abra o nó Autoridades de certificação raiz confiáveis e clique em Certificados).  Uma autoridade de certificação pode revogar o certificado de um editor se houver vazamento. Portanto, para verificar uma assinatura Authenticode, é necessário solicitar periodicamente à autoridade de certificação uma lista atualizada de revogações de certificação.  

Como o Authenticode usa assinatura criptográfica, uma assinatura Authenticode é inválida se alguém posteriormente mexer no arquivo.  

Como assinar com Authenticode

A primeira etapa é obter um certificado de assinatura de código de uma CA (consulte a barra lateral a seguir).  Você pode trabalhar com o certificado como um arquivo protegido por senha ou carregá-lo no armazenamento de certificados do computador.  O benefício de fazer isso é que você pode assinar sem precisar especificar uma senha.  Isso é vantajoso porque evita ter uma senha visível em scripts de construção automatizados ou arquivos em lote.  

ONDE OBTER UM CERTIFICADO DE ASSINATURA DE CÓDIGO 

Apenas algumas ACs de assinatura de código são pré-carregadas no Windows como autoridades de certificação raiz.  Isso inclui Comodo, Go Daddy, GlobalSign, DigiCert, thawte e Symantic.  Também existem revendedores, como a Ksoftware, que oferecem certificados de assinatura de código com desconto das autoridades mencionadas acima. 

Os certificados Authenticode emitidos pela Ksoftware, Comodo, Go Daddy e GlobalSign são anunciados como menos restritivos, pois também assinam programas que não são da Microsoft.  Além disso, os produtos de todos os fornecedores são funcionalmente equivalentes.  

Observe que um certificado para SSL geralmente não pode ser usado para assinatura Authenticode (apesar de usar a mesma infraestrutura X.509).  Isso ocorre, em parte, porque um certificado para SSL trata de provar a propriedade de um domínio;  Authenticode é sobre provar quem você é.  


Para carregar um certificado no armazenamento de certificados do computador, abra o Gerenciador de Certificados, conforme descrito anteriormente.  Abra a pasta Pessoal, clique com o botão direito do mouse na pasta Certificados e escolha Todas as Tarefas/Importar.  Um assistente de importação o guia pelo processo.  Após a conclusão da importação, clique no botão Visualizar no certificado, vá para a guia Detalhes e copie a impressão digital do certificado.  Esse é o hash SHA-256 que, posteriormente, você precisará identificar o certificado ao assinar. 

OBSERVAÇÃO 
Se você também deseja assinar com nome forte seu assembly, faça-o antes da assinatura do Authenticode.  Isso ocorre porque o CLR conhece a assinatura Authenticode, mas não o contrário.  Portanto, se você assinar um nome forte depois de assiná-lo pelo Authenticode, este verá a adição do nome forte do CLR como uma modificação não autorizada e considerará o conjunto violado.  

ASSINANDO COM SIGNTOOL.EXE 

Você pode autenticar seus programas com o utilitário signtool que acompanha o Visual Studio (consulte a pasta Microsoft SDKs\ClickOnce\SignTool em Arquivos de Programas).  A seguir, assina um arquivo chamado LINQPad.exe com o certificado localizado em Minha loja do computador, chamado "Joseph Albahari", usando o algoritmo de hash SHA256 seguro: 

signtool sign /n "Joseph Albahari" /fd sha256 LINQPad.exe 

Você também pode especificar um  descrição e URL do produto com /d e /du: 

... /d LINQPad /du http://www.linqpad.net 

Na maioria dos casos, você também desejará especificar um servidor de registro de data e hora.  

CARIMBO DE DATA/HORA 

Depois que seu certificado expirar, você não poderá mais assinar programas.  No entanto, os programas que você assinou antes de expirar ainda serão válidos - se você especificou um servidor de carimbo de data/hora com a opção /tr ao assinar.  A CA fornecerá um URI para essa finalidade: o seguinte é para a Comodo (ou Ksoftware): 

... /tr http://timestamp.comodoca.com/authenticode /td SHA256 

VERIFICAR QUE UM PROGRAMA FOI ASSINADO 

A maneira mais fácil de visualizar uma assinatura Authenticode em um arquivo é exibir as propriedades do arquivo no Windows Explorer (consulte a guia Assinaturas digitais).  O utilitário signtool também fornece uma opção para isso. 

Assinatura atrasada

É importante que as empresas protejam cuidadosamente a chave privada de seu par oficial de chaves públicas/privadas. Caso contrário, se pessoas não confiáveis o obtiverem, poderão publicar código mascarado como o código da empresa.  Para evitar isso, as empresas claramente não podem permitir acesso gratuito ao arquivo que contém seus dados públicos/par de chaves privadas. Nas grandes empresas, a nomeação forte de uma montagem é frequentemente realizada no final do processo de desenvolvimento, por um grupo especial com acesso ao par de chaves. Porém, isso pode causar problemas nos processos de desenvolvimento e teste pelos seguintes motivos:
1.	Como a chave pública é um dos quatro componentes da identidade de uma montagem, a identidade não pode ser definida até que o chave pública é fornecida.  
2.	Um assembly com nome fraco não pode ser implantado no GAC. Os desenvolvedores e os testadores precisam ser capazes de compilar e testar o código da maneira como ele será implantado na liberação, incluindo sua identidade e localização no GAC. Para permitir isso, existe uma forma modificada de atribuir um nome forte, chamado assinatura atrasada(delayed signing) ou parcial que supera esses problemas, mas não libera o acesso à chave privada.

 Na assinatura atrasada, o compilador usa apenas a chave pública do par de chaves pública/privada.  A chave pública pode então ser colocado no manifesto para completar a identidade da montagem.  Basicamente, você obtém o valor da chave pública da sua empresa em um arquivo e passa o nome do arquivo para qualquer utilitário usado para criar a montagem. Você pode usar a opção –p do SN.exe para extrair uma chave pública de um arquivo que contém um par de chaves pública/privada. Você também deve informar à ferramenta que deseja que o assembly seja atraso assinado, o que significa que você não está fornecendo uma chave privada. Para o compilador C#, você faz isso especificando a opção de compilador /delaysign. No Visual Studio, você exibe as propriedades do seu projeto, clique na guia Assinatura e marque a caixa de seleção Delay Sign Only. Se você estiver usando o AL.exe, poderá especificar a opção de linha de comando /delay[sign].

E você deve adicionar um atributo adicional chamado DelaySignAttribute ao escopo do assembly do código-fonte e defina seu valor como true. A Figura abaixo mostra a entrada e a saída para produzir uma montagem com atraso de assinatura.  
 

•	Na entrada, o DelaySignAttribute está localizado nos arquivos de origem e o arquivo-chave contém apenas a chave pública.
•	A assinatura atrasada também usa um bloco de 0s para reservar espaço para a assinatura digital.
•	Na saída, há espaço reservado para a assinatura digital na parte inferior do montagem.

Quando o compilador ou AL.exe detectar que você está atrasando a assinatura de um assembly, ele emitirá a entrada de manifesto AssemblyDef do assembly, que conterá a chave pública do assembly. Mais uma vez, a presença da chave pública permite que o assembly seja colocado no GAC. Também permite criar outros assemblies que fazem referência a esse assembly; os assemblies de referência terão a chave pública correta em suas entradas da tabela de metadados AssemblyRef. Ao criar a montagem resultante, resta espaço no arquivo PE resultante para a assinatura digital RSA. (O utilitário pode determinar quanto espaço é necessário a partir do tamanho da chave pública.) Observe que o conteúdo do arquivo também não será hash no momento.

Neste ponto, o assembly resultante não possui uma assinatura válida. A tentativa de instalar o assembly no GAC falhará porque um hash do conteúdo do arquivo não foi concluído - o arquivo parece ter sido violado. Em todas as máquinas nas quais o assembly precisa ser instalado no GAC, você deve impedir o sistema de verificar a integridade dos arquivos do assembly. Para fazer isso, use o utilitário SN.exe, especificando a opção de linha de comando –Vr. A execução do SN.exe com essa opção também informa ao CLR para ignorar a verificação de valores de hash para qualquer um dos arquivos do assembly quando carregados em tempo de execução. Internamente, a opção –Vr do SN adiciona a identidade do assembly sob a seguinte subchave do registro:

HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\StrongName\Verification.

Quando você terminar de desenvolver e testar o assembly, precisará assiná-lo oficialmente para poder empacotá-lo e implantá-lo. Para assinar o assembly, use o utilitário SN.exe novamente, desta vez com a opção –R e o nome do arquivo que contém a chave privada real. A opção –R faz com que o SN.exe faça o hash do conteúdo do arquivo, assine-o com a chave privada e incorpore a assinatura digital RSA no arquivo em que o espaço para ele havia sido reservado anteriormente. Após esta etapa, você pode implantar a montagem totalmente assinada. Nas máquinas de desenvolvimento e teste, não se esqueça de reativar a verificação desse assembly usando a opção de linha de comando –Vu ou –Vx do SN.exe.

A lista a seguir resume as etapas discutidas nesta seção para desenvolver sua montagem usando  a técnica de assinatura atrasada:
1.	Ao desenvolver uma montagem, obtenha um arquivo que contenha apenas a chave pública da sua empresa e  compile seu assembly usando as opções de compilador /keyfile e /delaysign.
csc /keyfile:MyCompany.PublicKey /delaysign MyAssembly.cs

2.	Se você tentar implantar o assembly assinado com atraso no GAC, o CLR não permitirá isso porque não está fortemente nomeada.  Para implantá-lo em uma máquina específica, depois de construir a montagem, você deve emitir um comando de linha de comando que desativa a verificação de assinatura do GAC nessa máquina, isso fará com que o CLR confie nos bytes do assembly sem executar o hash e a comparação somente para este conjunto, e permite que ele seja instalado no GAC.  Para fazer isso, emita o seguinte comando no prompt de comando do Visual Studio:
sn –vr MyAssembly.dll
Agora, você pode criar outras montagens que fazem referência à montagem e você pode testar a montagem.  Observe que você precisa executar apenas a seguinte linha de comando uma vez por máquina;  não é necessário executar esta etapa sempre que você criar sua montagem.

3.	Quando estiver pronto para empacotar e implantar a montagem, obtenha a chave privada da sua empresa e depois execute a seguinte linha.  Você pode instalar esta nova versão no GAC, se desejar, mas não tente instalá-lo no GAC até executar a etapa 4.
sn ra MyAssembly.dll MyCompany.PrivateKey

4.	Para testar em condições reais, ative novamente a verificação executando a seguinte linha de comando.
sn –vu MyAssembly.dll



 Agora você examinou assemblies com nome fraco, assemblies com atraso de assinatura e nome fortemente
 montagens.  A Figura abaixo resume as diferenças em suas estruturas.
 

No início desta seção, mencionei como as organizações mantêm seus pares de chaves em um dispositivo de hardware, como um cartão inteligente. Para manter essas chaves seguras, você deve garantir que os valores das chaves nunca sejam persistidos em um arquivo de disco. Os provedores de serviços criptográficos (CSPs) oferecem contêineres que abstraem a localização dessas chaves. A Microsoft, por exemplo, usa um CSP que possui um contêiner que, quando acessado, obtém a chave privada de um dispositivo de hardware.

Se o seu par de chaves pública/privada estiver em um contêiner CSP, você precisará especificar opções diferentes para os programas CSC.exe, AL.exe e SN.exe: Ao compilar (CSC.exe), especifique a opção /keycontainer em vez da opção /keyfile; ao vincular (AL.exe), especifique sua opção /keyname em vez da opção /keyfile; e ao usar o programa Strong Name (SN.exe) para adicionar uma chave privada a um assembly assinado com atraso, especifique a opção –Rc em vez da opção –R. O SN.exe oferece opções adicionais que permitem executar operações com um CSP.

Associar chave de nome forte à montagem

Quando uma chave de nome forte é gerada, ela deve ser associada a uma montagem para que ela possa se tornar uma montagem de nome forte. Quando o arquivo gerado é extensão pfx para associa-lo: 
 

Agora, para o arquivo snk, siga as seguintes etapas.
1)	Abra o arquivo AssemblyInfo.cs do projeto, (este arquivo está abaixo do arquivo Properties do Solution Explorer.)
2)	Associe uma chave de nome forte à montagem adicionando um atributo de montagem e o local de uma chave de nome forte, como [assembly: AssemblyKeyFile ("myClassLibrarykey.snk")]
3)	Pressione ctrl + shift + B para complilar a solução. Isso associará o par de chaves de nome forte à montagem. Lembre-se, o Visual Studio deve estar executando como administrador.
 

Hash da chave pública

Quando um assembly precisa ser assinado digitalmente, o compilador assina o assembly usando a chave privada e incorpora a chave pública no assembly para verificação posterior por outros assemblies que se referem a ele. O próximo passo é fazer o hash da chave pública para criar o Token de Chave Pública e incorporá-lo à montagem. Concluindo, o nome da montagem não é apenas o nome do arquivo ou o nome amigável. O nome completo de uma montagem é conhecido como Nome Totalmente Qualificado (FQN - Fully Qualiﬁed Name). Por exemplo, o FQN do assembly System no .NET 4.5 é

System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089

Versionamento lado a lado 
Implementando versões lado a lado

A nomeação forte teria pouco valor sem o poder de executar versões diferentes lado a lado do mesmo assembly. Considere o seguinte cenário. Na sua organização, existem duas equipes trabalhando em dois produtos diferentes, mas os dois precisam usar algumas funcionalidades comuns. Como mencionado anteriormente, a melhor maneira de lidar com esse tipo de situação é mover o código comum para um projeto separado, criando um assembly separado que pode ser usado pelas duas equipes. Tudo funciona bem; eles implantam seus produtos e há apenas uma cópia do conjunto comum por máquina, em vez de uma por produto. Depois de um tempo, a primeira equipe pode precisar de alguma funcionalidade adicional para ser adicionada ao assembly comum, mas a segunda equipe "ainda não está lá". Sem o controle de versão lado a lado, você deve manter uma cópia do assembly privada para cada instalação. Com dois produtos, essa pode ser uma solução aceitável, mas se você tiver mais produtos ou for um fornecedor de terceiros, será muito mais difícil manter esse tipo de solução. 

A versão lado a lado funciona apenas com assemblies com nomes fortes porque requer que eles sejam implantados no Global Assembly Cache (GAC). Mas, por enquanto, veja como funciona o controle de versão lado a lado. Quando você adiciona uma referência a um assembly, informações sobre o referido assembly são adicionadas ao arquivo de manifesto do assembly. O manifesto é incorporado ao assembly como parte dos metadados. Dentro do arquivo de manifesto, todo assembly referenciado é representado por um bloco semelhante ao código a seguir:

 

A primeira linha externa .assembly representa a referência ao assembly mscorlib, que é referenciado por padrão por todos os aplicativos .NET porque este contém todas as definições para todos os tipos de dados base no .NET. A versão do assembly é 4.0.0.0 e o Token de Chave Pública é (B7 7A 5C 56 19 34 E0 89).

O segundo .assembly extern representa uma referência a um assembly chamado CommonFunctionality que possui a versão 1.0.0.0 e não está assinado. Se a segunda assembly também tivesse sido assinada, as informações manifestas teriam algo assim:
	 
Essas informações são mostradas usando o aplicativo Intermediate Language Disassembler (ildasm.exe). 

ILdasm.exe 

O Desmontador Intermediário (ILdasm) é uma ferramenta usada para analisar qualquer assembly .NET em um formato legível por humanos.Essas informações analisadas são úteis para determinar todos os conjuntos de referência usados no conjunto especificado(.dll ou .exe). Ele também exibe os metadados, espaços para nome, tipos e interfaces usados no assembly. Use as seguintes etapas para analisar qualquer assembly no ildasm.exe:
1.	Crie e crie um aplicativo C# de console vazio.
2.	Vá para o menu Iniciar e abra o prompt de comando do Visual Studio.
3.	Use o prompt de comando do Visual Studio para navegar para a pasta raiz do projeto de console do C# que você acabou de criar. Por exemplo:
•	cd C:\Users\x_kat\Documents\Projetos\ClassLibrary_Recurso\bin\Debug
4.	No interior da pasta raiz do projeto, abra .exe ou .dll do arquivo do projeto com ildasm.exe digitando o comando ildasm {assemblyname}. Por exemplo:
•	ildasm ClassLibrary_Recurso.dll

Na imagem a seguir, você pode ver que o aplicativo é analisado no formato legível por humanos. Ele exibe o aplicativo MANIFEST, myConsoleApp. As informações do MANIFEST podem ser lidas clicando duas vezes nele. Da mesma forma, todo conteúdo do arquivo pode ser lido em um idioma intermediário clicando duas vezes nele.
 
 

  

Agora, quando você tenta executar o aplicativo, o tempo de execução tenta localizar os assemblies certos para você. Se um assembly não for assinado, o CLR procurará na pasta local do aplicativo para tentar encontrar um assembly com base apenas no nome e no assembly. nome do arquivo de montagem, ignorando a versão da montagem

 Se a montagem é assinada, o CLR tenta primeiro verificar se existem políticas especificadas para esse assembly específico que podem instruir o CLR a usar uma versão diferente. Esse processo de substituição de um assembly sem a necessidade de recompilar os assemblies que o utilizam é chamado de redirecionamento de ligação (binding redirection). Depois que a versão é estabelecida, o CLR primeiro procura no GAC essa versão específica do assembly e, se não estiver lá, procura na pasta local do aplicativo para tentar encontrar o assembly correto. Quando o CLR encontra a montagem, verifica a assinatura digital da montagem para garantir que a montagem não foi violada. Se o CLR encontrar o assembly, mas não encontrar a versão correta ou a assinatura não corresponder, ele lançará uma System.IO.FileLoadException. Se o CLR não puder encontrar o assembly, será lançada uma System.IO.FileNotFoundException.

Desde o .NET 2.0, a Microsoft adicionou a arquitetura do processador ao nome do assembly, que é opcional. Isso significa que você pode ter duas versões idênticas do mesmo assembly que diferem apenas pelo atributo ProcessorArchitecture. Os valores permitidos para a ProcessorArchitecture são descritos pela enumeração System.Reflection .ProcessorArchitecture. Os valores e seu significado são mostrados na Tabela abaixo.
Membro	Descrição
None	Não especificado ou desconhecido
MSIL	Processador independente
X86	Processador Intel 32-bit
IA64	Somente processador Intel 64-bit
Amd64	Somente processador AMD 64-bit
Arm	Processador ARM

O atributo de arquitetura do processador é mostrado apenas se tiver um valor diferente de Nenhum; portanto, se o assembly do Sistema que você viu anteriormente configurasse o ProcessorArchitecture como MSIL, o nome do assembly teria sido

System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, ProcessorArchitecture=MSIL

Resumo 
•	Um assembly é a saída do código compilado. Pode ser um arquivo .DLL ou .EXE.
•	Assembly público/comartilhado também é conhecida como assembléia com nome forte, são localizados no GAC e possui uma chave de nome forte associada a ela.
•	Reflexão é usada para ler atributos (metadados) para obter informações de todos os assemblies, módulos e tipos de um aplicativo em execução.
•	Atributos são um tipo de metadado para marcar código C# (tipos, métodos, propriedades e assim por diante).
•	Desmontador intermediário (ILdasm) é uma ferramenta usada para analisar qualquer assembly .NET em formato legível por humanos

Assembly e Reflection

Você pode carregar um assembly apenas para inspecioná-lo usando a classe MetadataLoadContext no .NET Core e os métodos Assembly.ReflectionOnlyLoad ou Assembly.ReflectionOnlyLoadFrom no .NET Core e .NET Framework para obter informações programaticamente sobre um assembly.


GAC - GLOBAL ASSEMBLY CACHE

Assemblies que são locais para um aplicativo são chamados assemblies privados. Você pode implantar facilmente um aplicativo que depende de assemblies privados, copiando-o para um novo local. Outra maneira de implantar um assembly é implantá-lo no Global Assembly Cache (GAC). O GAC é um local compartilhado comum de um sistema para armazenar assemblies de todos os aplicativos .NET executados em uma determinada máquina. Esses assemblies são compartilhados por vários aplicativos .NET no computador. No Windows ele esta localizado na pasta: C:\Windows\assembly, conforme figura abaixo.
 

Podemos fazer uma comparação com o modelo COM de registro de componentes. Se o componente não é encontrado no mesmo diretório da aplicação, a aplicação procura-o no GAC, do mesmo modo com o COM pesquisa o registro. Porém, os componentes .NET não precisam ser registrados como os componentes COM.

Se o assembly não for localizado no diretório local nem no GAC você ainda pode ter um endereço de localização no arquivo de configuração. A CLR pode então fazer o download do assembly e armazenar o assembly no cache de download. Você pode ter diferentes versões do mesmo assembly carregadas no GAC ao mesmo tempo, e, mesmo se um componente estiver rodando no GAC você pode incluir outra versão do mesmo componente.

Normalmente, você deseja evitar a instalação de assemblies no GAC. Algums vantagens para implantar no GAC seguem a seguir:
•	versionamento lado a lado
•	compartilhamento de assemblies por vários aplicativos. 
•	Se um assembly já estiver carregado na memória, o CLR não o carregará novamente.
•	segurança aprimorada: normalmente apenas usuários com direitos de administrador podem alterar o GAC.
•	Melhora no tempo de inicialização do seu aplicativo: a assinatura do assembly é verificada antes de ser instalada no GAC, portanto, quando o assembly será carregado pelo CLR quando for executado, ignora a verificação
•	pré-compilar os assemblies, para acelerar ainda mais o processo de inicialização, é possível pré-compilar os assemblies par que eles não precisem ser compilados pelo compilador JIT toda vez que você os carregar. Isso pode acelerar ainda mais o processo de inicialização executando um utilitário chamado ngen.exe (Native Image Generator). 

Para exibir uma lista de assemblies no cache de assembly global, abra o Prompt de Comando do Desenvolvedor para Visual Studio e, em seguida, digite o seguinte comando:
gacutil -l  OU  gacutil /l
 

Instalar seu próprio assembly

Um desenvolvedor pode instalar seu próprio assembly desenvolvido no GAC. Ele não apenas oferece a vantagem de compartilhar o mesmo assembly entre vários aplicativos .NET, mas também ajuda a fornecer segurança especial. Para colocar e remover assemblies do GAC você tem que ter privilégios de administrador do sistema por questão de segurança, além de que um assembly só pode ser instalado no GAC apenas se tiver um nome forte.Além da segurança, o assembly instalado no GAC permite arquivar e usar várias versões do mesmo assembly.
Existem cenários em que você explicitamente não deseja instalar um assembly no GAC
•	O Assembly deverá ser implantando privadamentee não será compartilhado entre várias outras aplicações
•	A implantação do XCOPY ou ClickOnce não pode ser usada porque são necessárias ações administrativas para instalar
•	A atualização de assemblies do GAC requer privilégios administrativos
•	Os testes podem se tornar mais difíceis
•	Versão e execução lado a lado se tornam mais complexas

A implantação de um assembly no GAC pode ser feita de duas maneiras:
1)	Para cenários de produção, 
•	Installers: Permite adicionarmos a instalação dos componentes dentro do GAC via Windows Installer. Isso é perfeitamente útil quando desejamos empacotar o sistema em um projeto de setup para que o usuário final, muitas vezes sem muito conhecimento, possa instalar o sistema sem maiores dificuldades. Este é a forma que se utiliza para a instalação de um componente em uma máquina cliente, já que o .NET Framework redistribuível não possui o utilitário GacUtil.exe.
Ao usar o instalador, você garante que a instalação seja atômica, o que significa que ele consegue instalar todos os componentes ou nenhum, e oferece a possibilidade de desinstalá-lo posteriormente da mesma maneira. Quando uma montagem é instalada dessa maneira, o instalador adiciona a montagem se não existir e, caso exista, aumenta apenas a contagem de referência, certificando-se de que o assembly não será desinstalado se ainda for usado quando você o desinstalar. 

2)	Em cenários de desenvolvimento,
•	Windows Explorer: 
Se navegar via Windows Explorer até o diretório C:\WINDOWS\ASSEMBLY você verá todos os Assemblies que estão no GAC daquela maquina específica. Você pode, via drag-and-drop, adicionar Assemblies ali, mas isso somente é interessante em ambiente de desenvolvimento.
•	Utilitário gacutil.exe: 
Este utilitário, fornecido também pelo SDK do .NET Framework, permite via linha de comando, interagir com o GAC, adicionando, removendo, listando, etc., Assemblies. Este utilitário é utilizando em ambientes de testes e desenvolvimento, nunca em produção.

Gacutil.exe

Gacutil.exe é uma ferramenta usada para instalar um assembly de nome forte em um cache global de assemblies. Siga as etapas a seguir para instalar um assembly de nome forte no GAC:
1)	Execute o prompt de comando do Visual Studio como administrador.
2)	Navegue até o diretório que se encontra a dll.
3)	Digite o seguinte comando para instalar um assembly de nome forte no GAC:
•	gacutil.exe [opções] [assemblyName | assemblyPath | assemblyListFile]
•	gacutil -i "{caminho do arquivo do nome forte assembly}.dll"
•	gacutil -i myClassLibrary.dll

A tabela abaixo mostra os parâmetros mais comuns para o gacutil.exe. Para uma referência completa, consulte a seção "Recursos e leituras adicionais"
Opção	Descrição
/i	Adiciona um assembly ao GAC.
/u	Remove uma montagem do GAC.
/l [assemblyName)	Lista todos os assemblies no GAC. Especificando o parâmetro assemblyName, listará apenas os assemblies correspondentes a esse nome.
/r	Rastreia referências a uma montagem aumentando um contador em cada
instalação e diminuindo o contador na desinstalação. Especifique esta opção com as opções /i, /il, /u ou /ul. Se uma montagem for rastreada, ela será removida do GAC somente quando o contador for 0.


WinMD Assembly

O conceito de montagem do WinMD foi introduzido quando o Windows 8 foi lançado. WinMD significa Windows Meta Data. Permite a comunicação entre diferentes linguagens de programação. Por exemplo, a biblioteca WinMD criada com C# pode ser usada em um projeto C++. Ele removeu a barreira do idioma através do Windows Runtime Component. O WinRT é completamente escrito em C++ nativo. Não há ambiente gerenciado, CLR e compilador Just-In-Time (JIT).

Embora o WinRT e o Windows RT sejam parecidos, eles são duas coisas completamente diferentes. O WinRT é o Windows Runtime. O Windows RT é uma versão especial do Windows 8 para dispositivos que usam processadores baseados em ARM. Esta versão do Windows é implantada em tablets, como o Microsoft Surface. Ele pode executar apenas aplicativos da Windows Store.

O Windows Runtime expõe uma maneira simples de criar componentes que podem ser usados por todos os idiomas suportados, sem a necessidade de uma complexa organização de dados. Uma biblioteca WinMD, chamada Windows Runtime Component, é um componente escrito em uma das linguagens WinRT (C#, VB ou C ++, mas não JavaScript) que pode ser usada por qualquer idioma suportado.
O desenvolvimento de aplicativos para o Windows 8, no entanto, pode ser feito em idiomas como JavaScript e C#. Um componente nativo C++ regular não inclui metadados. Mas os metadados são necessários para criar o mapeamento correto entre os componentes nativos e os outros idiomas.Para fazer isso funcionar, a Microsoft criou um novo tipo de arquivo chamado arquivos de metadados do Windows (WinMD).

Se você estiver executando o Windows 8, poderá encontrar esses arquivos localizados em 
C:\Windows\System32\WinMetadata.
 

O formato desses arquivos é o mesmo usado pelo .NET Framework para a CLI (Common Language Infrastructure). Os arquivos WinMD podem conter código e metadados. No entanto, os que você encontra no diretório System32 contêm apenas metadados. Esses metadados são usados pelo Visual Studio para fornecer o IntelliSense. No tempo de execução, os metadados informam à CLI que a implementação de todos os métodos encontrados neles é fornecida pelo tempo de execução. É por isso que os arquivos não precisam conter código real; eles garantem que os métodos sejam mapeados para os métodos corretos no WinRT.

Uma coisa a observar é que o WinRT não oferece acesso a todas as funcionalidades do .NET Framework 4.5. Em vez disso, muitas interfaces de programação de aplicativos (APIs) duplicadas, herdadas ou mal projetadas foram removidas. Isso tudo ajuda a garantir que os aplicativos WinRT possam ser portados para outras plataformas e usar apenas as melhores APIs disponíveis.

Se você deseja criar seu próprio assembly WinMD, faça isso criando um componente do Windows Runtime no Visual Studio. Você deve fazer isso apenas ao criar um componente que deve ser usado em diferentes linguagens de programação, como JavaScript e C#. Se você estiver trabalhando apenas com C#, crie um novo projeto “Class Library (Windows Store apps)”.

O componente Windows Runtime é compilado em um arquivo .winmd que você pode usar. Há algumas restrições no componente Windows Runtime que você precisa conhecer:
•	Os campos, parâmetros e valores de retorno de todos os tipos públicos e membros em seu componente devem ser do  Windows Runtime.
•	Classes e interfaces públicas podem conter métodos, propriedades e eventos. Uma classe pública ou interface não pode fazer o seguinte, no entanto:
o	Seja genérico
o	Implementar uma interface que não seja uma interface do Windows Runtime
o	Derive de tipos que não estão dentro do Windows Runtime
•	As aulas públicas devem ser seladas.
•	As estruturas públicas podem ter apenas campos públicos como membros, que devem ser tipos de valor ou strings.
•	Todos os tipos públicos devem ter um namespace raiz que corresponda ao nome do assembly e não inicie no Windows.

Se você criar um componente válido do Windows Runtime, poderá usar esta biblioteca ao criar um aplicativo do Windows 8. Dessa forma, você pode, por exemplo, criar um código complexo em C # e chamá-lo no seu aplicativo Windows Store da JavaScript.

Criar montagem do WinMD

O WinMD Assembly é usado em aplicativos da loja; para fazer o assembly do WinMD, você deve instalar o Windows 8.1 SDK. Neste ponto, suponho que o Visual Studio tenha o Windows 8.1 SDK instalado. Agora, para criar o assembly WinMD, siga estas etapas:
1.	Instale o suporte a desenvolvimento Universal Windows
2.	Abra o Visual Studio.
3.	Crie um novo projeto, selecione Visual C#
4.	Windows Store e selecione Windows Runtime Component como um modelo de projeto e selecione OK
 

5.	Selecione a versão
 

6.	Agora, crie um método estático que retorne a raiz quadrada de um valor inteiro

public sealed class Class1
{
    public static int square(int i)
    {
        return (i * i);
    }
}

7.	Crie o assembly e agora você pode usá-lo em qualquer modelo de aplicativo de loja de qualquer idioma, como VB.NET, F#, etc.



Observe que nas montagens Win md, todos os tipos devem ser selados e, se for necessário polimorfismo, use a interface no selado classificado para implementar o polimorfismo.

Resumo:
•	O GAC (Global Assembly Cache) é um repositório central para assemblies .NET
•	Assemblies compartilhados por vários aplicativos no computador devem ser armazenados no GAC
•	Os assemblies devem ser 'fortemente nomeados' para serem colocados no GAC Gerada usando a chave privada que corresponde à chave pública distribuída com o assembly e o próprio assembly
•	A nomeação forte de um assembly cria uma identidade exclusiva para o assembly ... não confie no nome forte para segurança

Depurar um aplicativo 
•	Criar e gerenciar diretrizes de pré-processador; escolher um tipo de compilação apropriado; gerenciar arquivos e símbolos de banco de dados do programa (símbolos de depuração)

Quando você estiver trabalhando no desenvolvimento de seu aplicativo, normalmente encontrará problemas inesperadosdevido a erros lógicos ou de tempo de execução, mesmo se o aplicativo estiver sendo testado ou em produção e você deseja monitorar a integridade de um aplicativo ou coletar as informações extras necessárias para identificarerros ou bugs. Para resolver esses problemas ou identificar e corrigir esses erros ou monitorar o aplicativo, C# e Microsoft Visual Studio nos ajudam nesse caso a fornecer classes e ferramentas para depuração e diagnóstico.

Debbuging

Depuração é o processo de identificação e remoção dos erros do seu aplicativo. O Visual Studio fornece a facilidade de interagir com essa execução virtual de código, onde é possível assistir a execução e alterações em andamento e executar outros recursos fornecidos como "Breakpoint", "Step over" e "Step into", etc. As alterações que estão ocorrendo à sua frente ajudam a identificar o erros de digitação ou de lógica. Você pode iniciar o processo de depuração para seu aplicativo pressionando F11 no Visual studio ou criar um ponto de interrupção para iniciar a depuração a partir do ponto específico

Modos de Compilação

Se você criar um novo projeto no Visual Studio, ele criará 2 modos de compilação padrão para você:
•	Release (lançamento):o código compilado é totalmente otimizado e nenhuma informação extra para fins de depuração é criada.
•	Debug (depuração): não há otimização aplicada e são fornecidas informações adicionais.

A diferença entre essas duas configurações é clara quando você executa o programa:

using System;
using System.Threading;

namespace Debugging
{
classProgram
    {
publicstaticvoid Main(string[] args)
        {
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            Console.ReadLine();
        }

privatestaticvoid TimerCallback(object o)
        {
            Console.WriteLine("In TimerCallback:" + DateTime.Now);
            GC.Collect();
        }
    }
}

Este aplicativo de console cria uma instância de um objeto Timer e, em seguida, define o timer para disparar a cada 2 segundos. Quando isso acontece, gera os dados e a hora atuais. Também chama GC.Collect para forçar a execução do coletor de lixo. Normalmente, você nunca faria isso, mas neste exemplo ele mostra algum comportamento interessante.

Quando você executa esse aplicativo no modo de depuração, ele executa um bom trabalho de saída do tempo a cada 2 segundos e continua fazendo isso até você finalizar o aplicativo.Mas quando você executa esse aplicativo no modo de liberação, ele exibe a data e a hora atuais apenas uma vez. 
  

Isso demonstra a diferença entre uma depuração e uma compilação de versão. Ao executar uma compilação de versão, o compilador otimiza o código. Nesse cenário, ele vê que o objeto Timer não é mais usado, portanto, não é mais considerado um objeto raiz e o coletor de lixo o remove da memória.

Na configuração de depuração, o compilador insere instruções extras de:
•	Não operação (No-Operation - NOP): são instruções que efetivamente não fazem nada (por exemplo, uma atribuição a uma variável que nunca é usada).
•	De ramificação: é um pedaço de código que é executado condicionalmente (por exemplo, quando alguma variável é verdadeira ou falsa). Quando o compilador vê que um determinado ramo nunca é executado, pode removê-lo da saída compilada. 

Ao otimizar o código, o compilador também pode optar por incorporar métodos curtos, removendo efetivamente um método da saída.

No mundo real, de repente você não precisa se preocupar com a coleta de lixo e o código errado no modo Release. O objeto Timer é um caso especial e normalmente você não teria problemas com isso. Mas ilustra a diferença entre uma versão e uma compilação de depuração. As informações extras que o compilador gera em uma compilação de depuração podem ser usadas para depurar seu aplicativo no Visual Studio.

Uma coisa que você pode fazer é definir um ponto de interrupção, para inspecionar e editar valores e influenciar o fluxo do seu programa: 
 

Enquanto você estiver trabalhando em seu aplicativo, a configuração Debug é a mais útil. Mas quando você estiver pronto para implantar seu aplicativo em um ambiente de produção, é importante garantir que você use a configuração Release para obter o melhor desempenho.

Diretivas de Compilador

Algumas linguagens de programação têm o conceito de um pré-processador, que é um programa que passa pelo seu código e aplica algumas alterações no seu código antes de entregá-lo ao compilador.O C# não possui um pré-processador especializado, mas suporta diretivas de compilador de pré-processador, que são instruções especiais para o compilador para ajudar no processo de compilação.

O C # suporta diretivas de compilador que são as instruções para ocompilador, que ajuda no processo de compilação. Essas diretivas dizem ao compilador como processar o código, são normalmente usadas para ajudar na compilação condicional. Em C #, eles não são usados para criar macros (fragmentos de código), diferentemente de C ou C ++.
As diretivas começam em "#" e não terminam com ponto-e-vírgula, pois não são declarações. Deve haver apenas uma diretiva de pré-processador em uma linha.

publicstaticvoid Main(string[] args)
{
    Timer t = new Timer(TimerCallback, null, 0, 2000);

#if DEBUG
        Debug.Write("Condition is True\n");
        Console.WriteLine("modo Debug");
#else
        Debug.Write("Condition is False\n");
        Console.WriteLine ("modo Release");
#endif

    Console.ReadLine();
}

A saída deste método depende da configuração de compilação usada. Se você definiu sua configuração de compilação como depuração, ela produz "Modo Debug"; caso contrário, mostra "modo Release".Ao usar a diretiva #if, você pode usar os operadores com os quais está acostumado em C#: == (igualdade),!= (desigualdade), && (e), || (ou) e ! (não) para testar se é verdadeiro ou falso.

Para utilizer o método Debug.Write é necessário fazer referência a System.Diagnostics, e o resultado é aparece na janela Output:
 

Quando o mesmo código é executado no modo "Release", ele não gera a mensagem "Condition is True"na janela OutPut, como a classe Debug e suas funções são ignoradas ou removidas pelo Modo Release ele é mais rápido que o Modo Debug e melhor em relação ao desempenho.

O símbolo de depuração é definido pelo Visual Studio quando você usa a configuração padrão para depuração. Isso é feito passando o comando /debug para o compilador. Você pode definir seus próprios símbolos usando a diretiva #define. 

#definir é apenas usado para definir um símbolo e deve estar na parte superior do arquivo.Não deve haver conflito entre o nome de um símbolo e uma variável e não se pode atribuir nenhum valor a um símbolo definido usando a diretiva #define.Os símbolos são normalmente condições usadas em #if ou #elif para testar a condição de compilação.

#if DEBUG
#define MyMessage
#else
#undef MyMessage
#warning Running with hard coded Tier information
#endif

using System;

namespace Diretiva_Precompilador
{
classProgram
    {
publicstaticvoid Main(string[] args)
        {
#if MyMessage
            System.Console.WriteLine("Cannot compile as the Tier is not specified");
#endif

            Console.ReadLine();
        }
    }
}

Outra diretiva de pré-processador que foi utilizada é #undef, que pode ser usada para remover a definição de um símbolo. Isso pode ser usado em uma situação em que você deseja depurar um pedaço de código que normalmente é incluído apenas em uma versão. Você pode usar a diretiva #undef para remover o símbolo de depuração.

Invés de definir um símbolo pelo diretiva (Ex.: #define Hamza), você também pode definir um símbolo nas opções do compilador, navegando até as propriedades do Project.
 

Freqüentemente, diretivas de pré-processador são usadas para incluir ou excluir um determinado trecho de código, dependendo da configuração da compilação. Utilize ConditionalAttribute para chamar uma determinada função apenas quando estiver criando uma configuração de depuração.

using System;
using System.Diagnostics;

namespace Diretiva_Precompilador
{
classProgram
    {
publicstaticvoid Main(string[] args)
        {
            Log("Step1");

            Console.ReadLine();
        }

        [Conditional("DEBUG")]
privatestaticvoid Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}

Outro atributo que pode ser útil ao depurar é DebuggerDisplayAttribute. Este atributo é usado pelo depurador do Visual Studio para exibir um objeto. 

[DebuggerDisplay("Name = {FirstName} {LastName}")]
publicclassPerson
{
publicstring FirstName { get; set; }
publicstring LastName { get; set; }
}
 

Diretiva	Descrição
#if	A diretiva #if compila o código entre as diretivas somente se o símbolo especificado estiver definido.
#else	A diretiva #else permite ao usuário criar uma diretiva condicional composta. Se nenhuma das expressões nas diretivas #if ou #elif anteriores for avaliada como true, o compilador avaliará todo o código entre #else e o #endif subsequente.
#elif	O #elif é uma diretiva condicional composta que é avaliada se nem o #if anterior nem nenhuma expressão opcional opcional da diretiva #elif forem avaliadas como verdadeiras.
#endif	A diretiva #endif especifica o final de uma diretiva condicional que começou com a diretiva #if.
#define	A diretiva #define é usada para definir um símbolo, que é uma sequência de caracteres.
#undef	A diretiva #undef permite que o usuário defina um símbolo.
#warning	A diretiva #warning é usada para gerar um aviso do compilador CS1030 nível um a partir de um local específico no código.
#error	A diretiva #error permite que o usuário gere um erro definido pelo usuário CS1029 a partir de um local específico no seu código.
#line	A diretiva #line permite ao usuário modificar a numeração das linhas do compilador e, opcionalmente, a saída do nome do arquivo para erros e avisos.
#region	A diretiva #region permite ao usuário especificar um bloco de código que será expandido ou recolhido ao usar o recurso de estrutura de tópicos do Visual Studio Code Editor.
#endregion	A diretiva #endregion marca o final de um bloco de #region.
#pragma	A diretiva #pragma fornece ao compilador instruções especiais para a compilação do arquivo no qual ele aparece.
#pragma warning	A diretiva de aviso #pragma pode ativar ou desativar certos avisos.
#pragma checksum	A diretiva #pragma checksum gera somas de verificação para arquivos de origem para ajudar na depuração de páginas ASP.NET.

Usar diretivas pode tornar seu código mais difícil de entender e, se possível, você deve evitá-las. Um cenário em que o uso de diretivas de pré-processador pode ser necessário é quando você está construindo uma biblioteca que visa várias plataformas. Ao criar uma biblioteca .NET direcionada a plataformas como Silverlight, WinRT e diferentes versões do .NET Framework, você pode usar as diretivas de pré-processador para suavizar as diferenças entre as plataformas.

O exemplo abaixo mostra as diferenças entre o WinRT e o .NET 4.5. no .NET 4.5, você pode obter o assembly de um tipo diretamente da propriedade Assembly. No WinRT, no entanto, essa API foi alterada e você precisa chamar GetTypeInfo. Usando uma diretiva de pré-processador, você pode reutilizar grande parte do seu código e ajustá-lo apenas para as diferenças.

using System;
using System.Reflection;

namespace Diretiva_Precompilador
{
classProgram
    {
publicstaticvoid Main(string[] args)
        {
//diferenças entre o WinRT e o .NET 4.5. no .NET 4.5
#if !WINRT
            Assembly assembly = typeof(int).Assembly;
            Console.WriteLine("!WINRT typeof(int).Assembly: " + assembly);
#else
            Assembly assembly = typeof(int).GetTypeInfo().Assembly;
             Console.WriteLine("!WINRT typeof(int).Assembly: " + assembly);
#endif

            Console.ReadLine();
        }
    }
}

Duas outras diretivas são #warning e #error. Você pode incluí-los no seu código para relatar um erro ou aviso ao compilador. O código abaixo mostra um exemplo.
 

Ao trabalhar com recursos de geração de código, algumas vezes você remove ou adiciona linhas em um arquivo de origem antes de ser compilado. Se ocorrer um erro no seu código, o compilador relatará um número de linha no seu arquivo que está fora de sincronia com a forma como você vê o código. A diretiva #line pode ser usada para modificar o número da linha do compilador e até o nome do arquivo. Você também pode ocultar uma linha de código do depurador. Se você depurar código usando a diretiva #line hide, o depurador ignorará as partes ocultas. 

	Cuidado ao utilizer a diretiva #line para renomear o arquivo como em acontece em #line 1 "Warning line.cs" pois isso fa com que o recurso de breakpoint não funcine mais.

using System;

namespace Diretiva_Precompilador
{
#line 1 "Warning line.cs"
classProgram
    {
publicstaticvoid Main(string[] args)
        {

#warning Warning from different filename

#line 200 "OtherFileName"
int a; // line 200
#linedefault
int b; // line 66
#linehidden
int c; // hidden
int d; // line 69

            Console.ReadLine();
        }
    }
}
Ao criar um aplicativo, você às vezes escreve voluntariamente algum código que aciona um aviso. Você não deseja alterar o código, mas deseja ocultar o aviso. Você pode fazer isso usando a diretiva de aviso #pragma. Você também pode optar por desativar ou restaurar avisos específicos. O compilador não relatará um aviso para a instrução int i, mas relatará um aviso para o código inacessível. 

using System;

namespace Diretiva_Precompilador
{
classProgram
    {
publicstaticvoid Main(string[] args)
        {

#pragmawarningdisable
int k; // Variavel não utilizada
#pragmawarningrestore

int j; // Variavel não utilizada

#pragmawarningdisable 0162, 0168
int i; // Variavel não utilizada
#pragmawarningrestore 0162

while (false)
            {
                Console.WriteLine("Unreachable code");
            }

#pragmawarningrestore

            Console.ReadLine();
        }
    }
}
 

Arquivo PDB

Ao compilar seus programas, você tem a opção de criar um arquivo extra com a extensão .pdb. Esse arquivo é chamado de Program Database File(PDB), que é uma fonte de dados extra que anota o código do aplicativo com informações adicionais que podem ser úteis durante a depuração.

Um arquivo PDB é um arquivo que contém informações sobre seu código ou programa usado pelo depurador para fins de depuração. Ele armazena uma lista de todos os símbolos presentes em um módulo (DLL ou EXE) junto com o número da linha em que são declarados e o endereço em que foram armazenados. Essas informações ajudam na depuração ou lançamento de erros em um local específico.

As informações incluídas em um arquivo PDB podem ser controladas usando as propriedades de um projeto. Para fazer isso,clique com o botão direito do mouse no projeto e selecione Propriedades. Clique no botão "Avançado" na guia Compilar. Um diálogo será abra onde você pode especificar as informações incluídas em um arquivo PDB; para usar “Informações sobre depuração”, suspensa para selecionar
 

Você pode construir o compilador para criar um arquivo PDB especificando as opções:
•	/debug: full: é a seleção padrão para o arquivo PDB no modo Debug. Um arquivo PDB é criado e o assembly específico possui informações de depuração
•	/debug: pdbonly: recomendada quando você está fazendo uma compilação Release. Um arquivo PDB é gerado sem modificação no assembly e o Visual Studio não inclui um atributo Debuggable eportanto, a depuração não pode ser executada.A razão pela qual um arquivo PDB é gerado no Modo de Release é ter informações para mensagens de exceção sobreonde ocorreu o erro, ou seja, rastreamento de pilha ou destino do erro, etc. Não é possível rastrear seu erros ou mensagem sem ter um arquivo PDB.
•	none: não gera um arquivo PDB e, portanto, você não pode depurar seu código mesmo no modo de depuração.

Um arquivo .NET PDB contém duas informações:
•	Nomes dos arquivos de origem e suas linhas
•	Nomes de variáveis locais

Esses dados não estão contidos nos assemblies do .NET, mas você pode imaginar como isso ajuda na depuração.Quando você carrega um módulo, o depurador começa a procurar o arquivo PDB correspondente. Ele faz isso procurando um arquivo PDB com o mesmo nome que fica no mesmo diretório que o aplicativo ou a biblioteca. Portanto, quando você possui um MyApp.dll, o depurador procura MyApp.pdb. Quando encontra um arquivo com um nome correspondente, ele compara um ID interno criado pelo compilador. O ID, que é um identificador global exclusivo (GUID), deve corresponder exatamente. Dessa forma, o depurador sabe que você está usando o arquivo PDB correto e pode mostrar o código-fonte correto para o seu aplicativo enquanto estiver depurando.

O importante é que esse GUID seja criado em tempo de compilação; portanto, se você recompilar seu aplicativo, obterá um novo arquivo PDB que corresponda exatamente à sua compilação recompilada. Portanto, você não pode depurar uma compilação de ontem usando o arquivo PDB que você criou hoje; os GUIDs não correspondem e o depurador não aceita o arquivo PDB.

Quando você executa uma sessão de depuração no Visual Studio, não há problemas na maior parte do tempo. Seu código e o aplicativo em execução correspondem exatamente, e o Visual Studio permite depurar o aplicativo. Mas quando você deseja depurar um aplicativo que está em produção no momento, é necessário o arquivo PDB correspondente para depurar o aplicativo.

Você pode ver os efeitos dos arquivos PDB ausentes ao executar o aplicativo de console e colocar um ponto de interrupção em algum lugar da função Main.Depois de atingir o ponto de interrupção, você pode abrir duas janelas interessantes. A primeira é a janela Módulos que você pode encontrar no menu Debug.
 

A janela Módulos mostra algumas coisas interessantes. Ele mostra uma lista de todas as DLLs necessárias para executar seu programa. Como você pode ver, apenas o último arquivo, PdbFiles.exe, possui um arquivo de símbolo correspondente carregado. Todos os outros têm Código de usuário definido como Não e a mensagem Símbolos de carregamento ignorados porque o depurador não consegue encontrar o arquivo PDB correspondente para esses módulos.

Outra área em que você perde os arquivos PDB é quando você olha na janela Pilha de chamadas (Call Stack) no menu Debug. A Figura abaixo mostra como é a janela Pilha de chamadas.
 
Como você pode ver, o depurador sabe que você está atualmente no método Main do seu aplicativo. Todos os outros códigos, no entanto, são vistos como Código Externo.

A Microsoft publicou com presteza seus arquivos PDB em seu Symbol Server, que é uma maneira de expor os arquivos PDB dos aplicativos ao depurador para que ele possa encontrar facilmente os arquivos. O Symbol Server também ajuda o depurador a lidar com as diferentes versões dos arquivos PDB, para que ele saiba como encontrar a versão correspondente para cada compilação.

Se você deseja usar o Microsoft Symbol Server, primeiro precisa desativar a opção Enable Just My Code (você pode encontrar essa opção em Tools → Options → Debugging → General). Diga ao depurador onde encontrar os arquivos de símbolos da Microsoft. Você pode fazer isso na mesma seção Opções, selecionando Symbols e, em seguida, selecionando a opção Microsoft Symbol Servers.

Quando você inicia a depuração, o depurador baixará os arquivos PDB do Microsoft Symbol Server. Se você olhar a janela Módulos, verá que todos os módulos têm seus símbolos carregados. Você também verá que a janela Call Stack mostra muito mais informações do que mostrava anteriormente.
 
Ao criar seus próprios projetos, é importante configurar um Symbol Server para seu uso interno. A maneira mais fácil de fazer isso é usar o Team Foundation Server (TFS) para gerenciar seu código-fonte e compilações. O TFS tem uma opção para publicar todos os arquivos PDB das suas compilações em um local compartilhado, que pode atuar como um Symbol Server para Visual Studio, permitindo depurar todas as versões anteriores de um aplicativo sem ter o código-fonte.

Lembre-se de como é importante salvar seus arquivos PDB em algum lugar. Se você jogá-los fora, perde imediatamente a oportunidade de depurar essa compilação específica do seu aplicativo.

Quando um arquivo PDB de tamanho completo é criado pelo compilador, ele contém duas coleções distintas de informações: dados de símbolos públicos e privados. Um arquivo de símbolo público contém menos dados. Expõe apenas os itens acessíveis de um arquivo de origem para outro. Os itens visíveis em apenas um arquivo de objeto, como variáveis locais, não estão na parte do símbolo público.

Ao publicar arquivos de símbolos para o mundo externo, como a Microsoft fez, você pode optar por remover as informações privadas. Quando você lida com propriedade intelectual que não deseja ser exposto, esse é um passo importante.

Você pode fazer isso usando a ferramenta PDBCopy. PDBCopy faz parte das Ferramentas de Depuração para Windows que você instala como parte do Windows Software Development Kit (SDK). A linha a seguir mostra um exemplo de remoção de dados privados de um arquivo PDB:
pdbcopy mysymbols.pdb publicsymbols.pdb –p

Esse código utiliza um arquivo mysymbols.pdb e cria um arquivo publicsymbols.pdb sem os dados do símbolo particular.

Implementar diagnóstico em um aplicativo 
•	Implementar log e rastreamento; criação de perfil de aplicativos; criar e monitor contadores de desempenho; escrever para o log de eventos

Como você lida com uma situação em que um aplicativo está em um ambiente de Produção e o usuário enfrenta alguns erros ou problemas relacionados ao desempenho de um aplicativo ou como rastrear onde o problema está ocorrendo? O Diagnostics ajuda você a lidar com essa situação, porque a Depuração não é útil para um ambiente de produção.

A depuração ajuda você no modo Debug, que normalmente usamos em uma fase de desenvolvimento, na qual podemos descobrir erros e corrigi-los; mas se o mesmo acontecer após o lançamento de um aplicativo ou quando ele estiver em uso real, poderemos diagnosticar nosso aplicativo para solucionar esses problemas. Embora a depuração remota seja possível (o que você pode fazer pelo seu aplicativo, mas o aplicativo deve estar hospedado), isso significa que você não pode fazer a depuração dos aplicativos offline. Além disso, para depuração remota, deve haver um Modo Debug ao publicar o aplicativo, o que não é preferível para liberar o aplicativo.

Para diagnosticar um aplicativo, normalmente fazemos instrumentação de nosso aplicativo, na qual diferentes abordagens podem ser usadas.

Instrumentando um Aplicativo

Para instrumentar um aplicativo, recursos de diagnóstico são adicionados a ele para estudar seu comportamento. Recursos de diagnóstico significa adicionar código para registro e rastreamento ou monitorar a integridade dos aplicativos. Isso permite rastrear a execução do programa (ou seja, qual erro ocorreu em qual local do código) e fornece o motivo de problemas relacionados ao desempenho sem a depuração.
Existem algumas maneiras de instrumentar seu aplicativo para realizar diagnósticos:
1.	Logging e rastreamento
2.	Perfil da Aplicação

Logging and tracing 

Quando seu aplicativo está sendo executado em um servidor de produção, às vezes é impossível anexar um depurador devido a restrições de segurança ou à natureza do aplicativo. Se o aplicativo for executado em vários servidores em um ambiente distribuído, como o Windows Azure, um depurador comum nem sempre ajudará a encontrar o erro.Por esse motivo, é importante que você implemente uma estratégia de registro (logging) e rastreamento (tracing) desde o início. 

O Tracing (rastreamento) é uma maneira de monitorar a execução do seu aplicativo enquanto ele está em execução. Você pode saber onde o erro está ocorrendo rastreando seu aplicativo e seguindo a execução do programa. Você pode acompanhar a execução do programa para saber quais métodos ele vai chamar, qual decisão está tomando, a ocorrência de erros e avisos etc. Ele fornece informações detalhadas para instigar um problema quando houver algum problema em um aplicativo.

O logging está sempre ativado e é usado para o relatório de erros. Você pode configurar seu log para coletar os dados de alguma maneira centralizada. Normalmente, é aplicado em um aplicativo em execução (em uso real) para receber mensagens informativas sobre a execução de um aplicativo. Talvez você queira um email ou mensagem de texto quando houver um problema sério. Outros erros podem ser registrados em um arquivo ou banco de dados.

O Tracing tem três fases principais:
1.	Instrumentação: Adicionando código de rastreamento no seu aplicativo.
2.	Rastreamento e registro: O código de rastreamento rastreia os problemas e grava em um destino especificado. O destino pode ser uma janela de saída, arquivo, banco de dados ou log de eventos.
3.	Análise: Depois de obter os problemas descritos em um formato específico ou gravados em um destino específico, você analisa os detalhes e identifica o problema.

Debug and Trace

O .NET Framework oferece 2 classes que podem ajudá-lo com o log e o rastreamento no namespace System.Diagnostics. 
•	A classe Debug: usada no modo compilação Debug para rastreamento e log. Isso ocorre porque o ConditionalAttribute com um valor DEBUG é aplicado à classe Debug
•	A classe Trace: usada no modo compilação Release para rastreamento e log.em um aplicativo que esteja em execução. 

Normalmente, as classes Debug e Trace usam Listeners para registrar erros rastreados. Essas classes fornecem alguns métodos para rastrear e colocam esses erros rastreados em arquivos, banco de dados ou EventLogs.
Debug	Trace	Description
Assert	Assert	Verifica a condição booleana e lança uma exceção em uma condição falsecom uma pilha de chamadas (informações rastreadas sobre erro).
Flush
	Flush
	Liberta os buffers e coloca os dados com buffer de gravação nos Listeners.
Indent	Indent	Aumenta o recuo em um nível.
UnIndent	UnIndent	Diminui o recuo em um nível.
Write	Write	Grava na mensagem para rastrear o ouvinte.
WriteIf	WriteIf	Aceita a condição e grava a mensagem para rastrear o ouvinte se o condição é verdadeira.
WriteLine	WriteLine	Grava a mensagem na coleção Listeners do Debug pelo terminador de linha.
É uma função sobrecarregada e fornece mais interatividade para registrar o erro rastreado.
WriteLineIf	WriteLineIf	Comporte-se do mesmo modo do método WriteLine, mas faz o processo com base em um
condição

Você pode usar essas funções para rastrear e imprimir os erros (log) e usar a instância do Listener (TraceListeners fornecidos em C#) para registrar informações rastreadas para um destino específico.

staticvoid Main(string[] args)
{

try
    {
        Debug.WriteLine("Starting application");
int age = 10;
        Debug.WriteLineIf(age.GetType() == typeof(int), "Age is Valid");
for (int k = 0; k < 5; k++)
        {
            Debug.WriteLine("Loop executed Successfully");
        }

        Debug.Indent();
int i = 1 + 2;
        Debug.Assert(i == 3);
        Debug.WriteLineIf(i > 0, "i is greater than 0");
        Debug.Print("Tracing Finished");
    }
catch (Exception)
    {
        Debug.Assert(false);
    }
}
 
Por padrão, a classe Debug grava sua saída na janela Saída no Visual Studiocomo ouvinte de rastreamento padrão. Se a instrução Debug.Assert falhar, você receberá uma caixa de mensagem mostrando o rastreamento de pilha atual do aplicativo. Esta caixa de mensagem solicita que você tente novamente, anule ou ignore a falha de afirmação. Você pode usar Debug.Assert para indicar um bug no seu código que você deseja apontar enquanto desenvolve seu aplicativo.

TraceSource

Outra classe que você pode usar é a classe TraceSource, que foi adicionada no .NET 2.0 e deve ser usada no lugar da classe estática Trace. A classe Trace pode ser usada tanto no modo Release quanto no modo Debug, pois ele roda em um thread diferente, enquanto o Debug é executada no thread principal. Porém, a classeTraceSource
oferece mais interatividade com os problemas em um aplicativo quando ele está em execução da Produção, ao contrário da classe Debug e da Trace.

A classe TraceSource fornece um conjunto de métodos e propriedades que permitem que os aplicativos rastreiem a execução do código e associe mensagens de rastreamento à sua origem.
Propriedades	Descrição
Attributes
Obtém os atributos de opção personalizados definidos no arquivo de configuração de aplicativo.
Listeners
Obtém a coleção de ouvintes de rastreamento para a origem de rastreamento.
Name
Obtém o nome da origem de rastreamento.
Switch
Obtém ou define o valor da opção de fonte.


Métodos	Descrição
Close()
Fecha todos os ouvintes de rastreamento na coleção de ouvintes de rastreamento.
Flush()
Libera todos os ouvintes de rastreamento na coleção de ouvintes de rastreamento.
TraceData(TraceEventType, Int32, Object)
Grava dados de rastreamento para os ouvintes de rastreamento na coleção Listeners usando o tipo de evento, o identificador de evento e os dados de rastreamento especificados.
TraceData(TraceEventType, Int32, Object[])
Grava dados de rastreamento nos ouvintes de rastreamento na coleção Listeners usando o tipo de evento, o identificador de evento e a matriz de dados de rastreamento especificados.
TraceEvent(TraceEventType, Int32)
Grava uma mensagem de evento de rastreamento para os ouvintes de rastreamento na coleção Listeners usando o tipo de evento e o identificador de evento especificados.
TraceEvent(TraceEventType, Int32, String)
Grava uma mensagem de evento de rastreamento para os ouvintes de rastreamento na coleção Listeners usando o tipo de evento, o identificador de evento e a mensagem especificados.
TraceEvent(TraceEventType, Int32, String, Object[])
Grava um evento de rastreamento para os ouvintes de rastreamento na coleção Listeners usando o tipo de evento, o identificador de evento, a matriz de argumentos e o formato especificados.
TraceInformation(String)
Grava uma mensagem informativa para os ouvintes de rastreamento na coleção Listeners usando a mensagem especificada.

TraceInformation(String, Object[])
Grava uma mensagem informativa nos ouvintes de rastreamento na coleção Listeners usando a matriz de objetos e as informações de formatação especificadas.
TraceTransfer(Int32, String, Guid)
Grava uma mensagem de transferência de rastreamento nos ouvintes de rastreamento na coleção Listeners usando o identificador numérico especificado, a mensagem e o identificador de atividade relacionada.

staticvoid Main(string[] args)
{

try
    {
        Trace.WriteLine("Tracing Start:Numbers must be Int");
int num1 = 10;
int num2 = 0;

        Trace.WriteLineIf(num1.GetType() == typeof(int) &&
                            num2.GetType() == typeof(int), "Numbers are valid");
if (num2 < 1)
        {
            num2 = num1;
            Trace.TraceInformation("num2 has been changed due to zero value");
        }

int result = num1 / num2;
        Trace.Indent();

        TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);

        traceSource.TraceInformation("Tracing application..");
        traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
        traceSource.TraceData(TraceEventType.Information, 1, newobject[] { "a", "b", "c" });
        traceSource.Flush();
        traceSource.Close();

        Console.ReadKey();
    }
catch (Exception ex)
    {
        Trace.Assert(false);
        Trace.TraceError(ex.Message);
    }
}
 

Como você pode ver, você pode passar um parâmetro do tipo TraceEventType para os métodos de rastreamentoTraceEvent e TraceData. 

public TraceEvent (System eventType, int id, string message )

Essas informações são posteriormente usadas pelo TraceSource para determinar quais informações devem ser exibidas.Você pode usar várias opções diferentes para a enumeração TraceEventTypepara especificar a gravidade do evento que está acontecendo.
TraceEventType	Descrição
Critical	Essa é a opção mais grave. Deve ser usado com moderação e apenas para erros muito graves e irrecuperáveis.
Error	Este membro da enumeração tem uma prioridade um pouco menor que Crítica, mas ainda indica que algo está errado no aplicativo. Normalmente, ele deve ser usado para sinalizar um problema que foi tratado ou recuperado.

Warning	Este valor indica que ocorreu algo incomum que pode valer a pena investigar mais. Por exemplo, você percebe que uma determinada operação demora subitamente a processar mais do que o normal ou sinaliza um aviso de que o servidor está ficando com pouca memória.
Information	Este valor indica que o processo está sendo executado corretamente, mas há algumas informações interessantes a serem incluídas no arquivo de saída de rastreamento. Pode ser que o usuário tenha feito login no sistema ou que algo tenha sido adicionado ao banco de dados.
Verbose	Este é o mais baixo de todos os valores relacionados à gravidade na enumeração. Ele deve ser usado para informações que não indicam nada de errado com o aplicativo e podem aparecer em grandes quantidades. Por exemplo, ao instrumentar todos os métodos em um tipo para rastrear seu início e final, é comum usar o tipo de evento detalhado.
Stop, Start, Suspend, Resume, Transfer:	Esses tipos de eventos não são indicações de gravidade, mas marcam o evento de rastreamento como relacionado ao fluxo lógico do aplicativo. Eles são conhecidos como tipos de eventos de atividade e marcam o início ou a parada de uma operação lógica ou a transferência de controle para outra operação lógica.

O segundo argumento para os métodos de rastreamento é o número do ID do evento. Este número não tem nenhum significado predefinido; é apenas outra maneira de agrupar seus eventos. Você pode, por exemplo, agrupar as chamadas de seu banco de dados como números 10000 a 10999 e o serviço da Web como 11000 a 11999 para saber mais facilmente qual área do seu aplicativo uma entrada de rastreamento está relacionada.

O terceiro parâmetro é uma sequência que contém a mensagem que deve ser rastreada. Ao usar o método TraceData, você pode passar argumentos extras que devem ser gerados no rastreamento.

Listeners

Gravar todas as informações na janela Saída pode ser útil durante as sessões de depuração, mas não em um ambiente de produção. Para alterar esse comportamento, as classes Debug e TraceSource têm uma propriedade chamada Listeners. Essa propriedade contém uma coleção de TraceListeners, que processam as informações dos métodos Write, Fail e Trace.

Prontamente, as classes Debug e TraceSource usam uma instância da classe Default¬TraceListener. O DefaultTraceListener grava na janela Saída e mostra a caixa de mensagem quando a asserção falha.

Os Listeners criados devem refletir as necessidades do aplicativo. Por exemplo, se desejar obter um registro de texto de toda a saída de rastreamento, crie um Listener TextWriterTraceListener, que grava toda a saída para um novo arquivo de texto quando ele é habilitado. Por outro lado, se desejar exibir a saída somente durante a execução do aplicativo, crie um Listener ConsoleTraceListener, que direciona toda a saída para uma janela do console. O EventLogTraceListener pode direcionar a saída de rastreamento para um log de eventos.

Você pode usar vários outros TraceListeners que fazem parte do .NET Framework. 
Nome	Resultado
ConsoleTraceListener	Saída padrão ou fluxo de erro
DelimitedListTraceListener	TextWriter
EventLogTraceListener	EventLog
EventSchemaTraceListener	Arquivo de log codificado em XML e compatível com o esquema
TextWriterTraceListener	TextWriter
XmlWriterTraceListener	Dados codificados em XML para um TextWriter ou fluxo.

ConsoleTraceListener

Esse ouvinte registra os erros ou os gera em uma tela do console (Target), que é uma saída padrão. Repare que no exemplo abaixo está implementado o Filter do TraceListener.

staticvoid Main(string[] args)
{
    Console.WriteLine("Hello World!");

using (var console_listener = new ConsoleTraceListener())
    {
//// limpa o ouvinte trace padrão
        Trace.Listeners.RemoveAt(0);

//// adicionando o ouvinte
        Trace.Listeners.Add(console_listener);

if (args.Length >= 1)
        {
string failMessage = String.Format("\"{0}\" " +
"is not a valid number of possibilities.", args[0]);
            console_listener.Fail(failMessage, "erro message");
        }
else
        {
// Report that the required argument is not present.
conststring ENTER_PARAM = "Enter the number of " +
"possibilities as a command line argument.";
            console_listener.Fail(ENTER_PARAM);
        }


// especifique a fonte de rastreamento
        TraceSource ts = new TraceSource("ConsoleTraceSource", SourceLevels.All);

        console_listener.Name = "console_listener";
// limpa o ouvinte padrão
        ts.Listeners.Clear();
// adicionando o ouvinte
        ts.Listeners.Add(console_listener);
// rastreando as informações / problemas que entrarão no ouvinte adicionado
        ts.TraceInformation("Rastreio TraceSource a aplicação..");

// Test the filter on the ConsoleTraceListener.
        ts.Listeners["console_listener"].Filter = new SourceFilter("No match");
        ts.TraceData(TraceEventType.Error, 5,
"\nSourceFilter should reject this message for the console trace listener.");
        ts.Listeners["console_listener"].Filter = new SourceFilter("ConsoleTraceSource");
        ts.TraceData(TraceEventType.Error, 6,
"\nSourceFilter should let this message through on the console trace listener.");

        ts.TraceData(TraceEventType.Error, 1, newstring[] { "Erro1", "Erro2" });
        ts.Flush();
        ts.Close();
    }

    Console.ReadKey();
}

DelimitedListTraceListener

Direciona a saída de rastreamento ou depuração para um gravador de texto, como um gravador de fluxo ou para um fluxo, como um fluxo de arquivos.

O Delimiter Obtém ou define o delimitador para a lista delimitada. O delimitador padrão é ";" (ponto e vírgula).

O TraceOutputOptions 	obtém ou define as opções de saída de rastreamento.
Campo	Enum	Descrição
Callstack 	32	Grave a pilha de chamadas, que é representada pelo valor retornado da propriedade StackTrace.

DateTime 	2	Grave a data e hora.
LogicalOperationStack 	1	Grave a pilha de operação lógica, que é representada pelo valor retornado da propriedade LogicalOperationStack.

None 	0	Não grave todos os elementos.
ProcessId 	8	Grave a identidade do processo, que é representada pelo valor retornado da propriedade Id.

ThreadId 	16	Grave a identidade do thread, que é representada pelo valor retornado da propriedade ManagedThreadId para o thread atual.

Timestamp 	4	Grave o carimbo de data/hora, que é representado pelo valor retornado do método GetTimestamp().



staticvoid Main(string[] args)
{
using (var delimited_listener = new DelimitedListTraceListener("test.txt"))
    {
var mySource = new TraceSource("Testsource", SourceLevels.All);
// limpa o ouvinte padrão
        mySource.Listeners.Clear();

        delimited_listener.Name = "delimited_listener";
        delimited_listener.Delimiter = ",";
        mySource.Listeners.Add(delimited_listener);

//mySource.Listeners["delimited_listener"].Filter = new SourceFilter("No match");
//mySource.Listeners["delimited_listener"].Filter = new SourceFilter("Testsource");

        mySource.TraceData(TraceEventType.Information, 1, "x");
        mySource.TraceInformation("y");

        mySource.TraceEvent(TraceEventType.Error, 2, "z");
        mySource.TraceInformation("w");
        mySource.Flush();
        mySource.Close();

//No modo Debug Imprime as 2 mensagens na janela Output
//No modo Release Imprime somente a mensagm do Trace
        Debug.Listeners.Add(delimited_listener);
        Debug.WriteLine("Saida DelimitedListTraceListener no Debug");
        Debug.Flush();

        Trace.Listeners.Add(delimited_listener);
        Trace.WriteLine("Saida DelimitedListTraceListener no Trace");
        Trace.Flush();
    }

    Console.ReadKey();
}

 


	Abaixo um exemplo utlilizando stream (FileStream), a propriedade Delimiter e TraceOutputOptions

staticvoid Main(string[] args)
{
    FileStream stream = new FileStream("test_stream.txt", FileMode.Create, FileAccess.Write); // = FileStream.Null;
    StreamWriter sw = new StreamWriter(stream);

using (var stream_delimited_listener = new DelimitedListTraceListener(sw))
    {
        TraceEventCache cc = new TraceEventCache();

        stream_delimited_listener.Delimiter = ":";
        stream_delimited_listener.TraceData(cc, null, TraceEventType.Error, 7, "XYZ");
        stream_delimited_listener.TraceData(cc, null, TraceEventType.Error, 7, "ABC", "DEF", 123);
        stream_delimited_listener.TraceEvent(cc, null, TraceEventType.Error, 4, null);

        stream_delimited_listener.TraceOutputOptions = TraceOptions.ProcessId | TraceOptions.ThreadId | TraceOptions.DateTime | TraceOptions.Timestamp;

        stream_delimited_listener.TraceData(cc, null, TraceEventType.Information, 1, "x");
        stream_delimited_listener.TraceEvent(cc, null, TraceEventType.Error, 4, null);
    }

    Console.ReadKey();
}

 

EventLogTraceListener

Ao lado de gravar informações de rastreamento em um arquivo ou banco de dados, você também pode gravar eventos no Log de Eventos do Windows. Você faz isso usando a classe EventLog no namespace System.Diagnostics. 

EventLog é uma classe usada para acessar EventLogs, que registra informações sobre o evento importante de um aplicativo. Você pode ler os logs existentes, criar novos logs e gravá-los ou criar e excluir uma fonte de eventos.

Para usar a classe EventLog, você precisa executar o Visual Studio como administrador para obter as permissões apropriadas para criar logs de eventos,senão dará erro no método EventLog.SourceExists.
 

staticvoid Main(string[] args)
{
string sourceName = "Sample Log";
string logName = "Application";
string machineName = ".";// . means local machine
string entryTowritten = "Some random entry into Event Log";

if (!EventLog.SourceExists(sourceName, machineName))
    {
        EventLog.CreateEventSource(sourceName, logName);
    }

    EventLog.WriteEntry(sourceName, entryTowritten, EventLogEntryType.Information);

    Console.ReadKey();
}

 
O método CreateEventSource é usado para criar a fonte de um evento (criação de um novo EventLog) com o nome fornecido. O nome do log, que é "Application", é opcional. Log é uma categoria ou algo semelhante a um arquivo no qual as entradas de origem devem ser gravadas. Existem três tipos de logs: 
1.	Application: que registra eventos provenientes de aplicativos registrados. Os aplicativos pode criar seus próprios logs, como é o caso dos programas Active Directory e DNS Server, mas na maioria dos casos eles gravam seus eventos no Application log do sistema.
2.	System: que registra eventos que ocorrem nos componentes do sistema, como drivers
3.	Security: que registra as alterações de segurança e tenta violar as permissões de segurança
4.	Custom log: um novo log criado automaticamente qunado os oito primeiros caracteres do nome do log definido na criação não correponderem a nenhum dos logs padrões.

Estão disponíveis cinco tipos diferentes de eventos:
•	Information: Uma operação bem-sucedida significativa - por exemplo, quando um serviço é iniciado ou quando uma operação de backup complexa é concluída. Este é o tipo padrão que você não especifica de outra forma.
•	Warning: Ocorreu um problema, mas o aplicativo pode se recuperar dele sem ser desligado. Um aviso típico registra uma situação de poucos recursos, que pode causar problemas posteriores, como perda de desempenho.
•	Error: Ocorreu um problema significativo, como perda de dados ou funcionalidade: por exemplo. O Windows grava um evento de erro quando não pode carregar um serviço.
•	Sucess audit (Auditoria de sucesso): Um evento de segurança que ocorre quando uma tentativa de acesso é bem-sucedida, como, por exemplo, quando um usuário efetua logon na máquina.
•	Failure audit (Auditoria com falha): Um evento de segurança que ocorre quando uma tentativa de acesso falha, como quando um usuário não pode fazer logon ou quando um arquivo não pode ser aberto, porque o usuário tem permissões de segurança insuficientes.

Para gravar dados ou uma entrada em um EventLog, use o método WriteEntry, no qual é necessário saber o que escrever, onde escrever e qual deve ser o nível de dados a ser gravado.Para visualizar o log criado, abra o Visualizador de Eventos, indo em "Iniciar" no Windows >>“Administrative Tools”. Quando a janela estiver aberta, abra o aplicativo "Event Viewer".
 
No lado esquerdo (painel do menu) do Visualizador de Eventos, há uma pasta ("Logs do Windows"), que contém todos os logs do tipo Aplicativo e Sistema (nome do log que especificamos em nosso código). Os logs com um nome de log personalizado vão para a pasta "Logs de aplicativos e serviços".

Clique no registro "Aplicativo", conforme especificado no nome do registro "Application" no código. Pesquise todos os logs com o nome fornecido no código, que é Sample Log. Clique nisso. Isso parecerá assim:
 

Os destaques na figura acima mostram que nossas informações especificadas no código estão no EventLog. Sempre que o código é executado, uma nova entrada é criada com a mensagem específica dentro do log fornecido.

Custom Log

Se o nome do log que você passa no segundo argumento do método CreateEventSouroe ou para o construtor do EventLog não corresponda a um log existente, um novo log é criado automaticamente para você. Isso também significa que você pode criar acidentalmente novos logs quando é digitado incorretamente o nome do log

Somente os oito primeiros caracteres do nome do log são significativos, se ester primeiros caracteres corresponderem ao nome de um log existente (como no ApplicationNewLog), você não criará um novo log. Você pode criar um log personalizado em uma máquina remota, passando um terceiro argumento para o método CreateEventSource, desde que você tenha direitos administrativos suficientes no sistema remoto.

Você exclui um log customizado usando o método compartilhado EventLog.Delete. Preste atenção ao usar esse método, pois ele exclui todas as entradas de eventos e todas as fontes de eventos associadas ao log excluído. Observe também que você pode excluir acidentalmente um dos logs predefinidos do sistema; nesse caso, você precisará reinstalar todo o sistema operacional novamente.

string sourceName = "Sample Log";
string logName = "Application";
string machineName = ".";// . means local machine
EventLog.Delete(logName, machineName)

Uma operação menos radical consiste em remover todas as entradas de um determinado log chamando o método Clear:

EventLog log = new EventLog(logName, machineName, sourceName);
log.Clear();

Limpar um log de eventos periodicamente - possivelmente após salvar seu conteúdo atual em um arquivo a partir do utilitário Visualizador de Eventos - ajuda a evitar problemas quando o log de eventos fica cheio, quando isso acontece é começar a sobrescrever as entradas mais antigas pelas mais recentes. Por padrão, os logs Application, System e Security têm um tamanho máximo padrão de 4992 KB enquanto os logs personalizados podem crescer até 512 KB. Você pode alterar esses valores padrão e modificar o comportamento padrão no utilitário Visualizador de Eventos.

Você também pode ler o EventLog. Por exemplo, preciso ler as informações mais recentes do log de amostra. O código a seguir ajuda você:

staticvoid Main(string[] args)
{
string sourceName = "Sample Log";
string logName = "Application";
string machineName = ".";// . means local machine

    EventLog log = new EventLog(logName, machineName, sourceName);
    Console.WriteLine("Total entries: " + log.Entries.Count);
    EventLogEntry last = log.Entries[log.Entries.Count - 1];//last(latest) log com nome "Sample Log" 

    Console.WriteLine("Index: " + last.Index);
    Console.WriteLine("Source: " + last.Source);
    Console.WriteLine("Type: " + last.EntryType);
    Console.WriteLine("Time: " + last.TimeWritten);
    Console.WriteLine("Message: " + last.Message);
    Console.WriteLine("Machine Name: " + last.MachineName);
    Console.WriteLine("Category: " + last.Category);

    Console.ReadKey();
}

O EventLog também oferece a opção de assinar alterações no log de eventos. Ele expõe um evento EntryWritten especial no qual você pode se inscrever para alterações. Você pode usar isso, por exemplo, para alertar os administradores do sistema sobre situações críticas, para que eles não precisem monitorar o log de eventos manualmente.

staticvoid Main(string[] args)
{
string sourceName = "Sample Log";
string logName = "Application";
string machineName = ".";// . means local machine

    EventLog log = new EventLog(logName, machineName, sourceName);
    log.EntryWritten += (sender, e) =>
    {
        Console.WriteLine(e.Entry.Message);
    };
    log.EnableRaisingEvents = true;
    log.WriteEntry("Test message", EventLogEntryType.Information);

    Console.ReadKey();
}

Quando o rastreamento precisa ser registrado, usamos o EventLogTraceListener para registrar dados rastreados ou depurados no destino correspondente (EventLog).

staticvoid Main(string[] args)
{
string sourceName = "Sample Log";
string logName = "Application";
string machineName = ".";// . means local machine
//Creation of log
if (!EventLog.SourceExists(sourceName, machineName))
    {
        EventLog.CreateEventSource(sourceName, logName);//EventLog created
    }

//Specifing created log (target)
    EventLog log = new EventLog(logName, machineName, sourceName);

//specify the EventLog trace listener
using (var eventLog_listener = new EventLogTraceListener())
    {
//specify the target to listener
        eventLog_listener.EventLog = log;
        eventLog_listener.Name = "eventLog_listener";

//specifing the Trace class, just to trace information
        TraceSource trace = new TraceSource("eventLog_SampleSource", SourceLevels.Information);
//Clearing default listener
        trace.Listeners.Clear();
//assigning new listener
        trace.Listeners.Add(eventLog_listener);
//Start tracing

// Test the filter on the ConsoleTraceListener.
//Como foi setado na configuração do TraveSource  SourceLevels.Information
// deveria bloquear a saída do TraceData....o que não ocorre
        trace.Listeners["eventLog_listener"].Filter = new SourceFilter("No match");
        trace.TraceData(TraceEventType.Error, 5,
"\nSourceFilter should reject this message for the eventLog trace listener.");
        trace.Listeners["eventLog_listener"].Filter = new SourceFilter("eventLog_SampleSource");
        trace.TraceData(TraceEventType.Error, 6,
"\nSourceFilter should let this message through on the eventLog trace listener.");

        trace.TraceInformation("Tracing start to Event Log");
        trace.Flush();
        trace.Close();
    }

    Console.ReadKey();
}

 
  

EventSchemaTraceListener

Direciona a saída do rastreamento ou da depuração de eventos de ponta a ponta para um arquivo de log em conformidade com o esquema, codificado em XML.

A EventSchemaTraceListener classe fornece rastreamento de eventos de ponta a ponta em conformidade com o esquema. Você pode usar o rastreamento de ponta a ponta para um sistema que tem componentes heterogêneos que cruzam os limites AppDomain de thread, processo e computador. Um esquema de evento padronizado foi definido para habilitar o rastreamento entre esses limites. O esquema permite a adição de elementos personalizados em conformidade com o esquema. 

A EventSchemaTraceListener classe herda a Filter propriedade da classe TraceListenerbase. A Filter propriedade permite um nível adicional de filtragem de saída de rastreamento no ouvinte. Se um filtro estiver presente, os Trace métodos do ouvinte de rastreamento chamarão o ShouldTrace método do filtro para determinar se o rastreamento deve ser emitido.

#define NOCONFIGFILE

[STAThreadAttribute]
staticvoid Main(string[] args)
{
string fileName = "TraceOutput.xml";
string event_name = "eventschema_listener";

    File.Delete(fileName);
    TraceSource ts = new TraceSource("TestSource");

//specify the EventSchema trace listener
using (var eventschema_listener = new EventSchemaTraceListener(fileName, event_name))
    {

#if NOCONFIGFILE
//ts.Listeners.Add(new EventSchemaTraceListener(fileName, event_name, 65536, TraceLogRetentionOption.LimitedCircularFiles, 20480000, 2));
        ts.Listeners.Add(eventschema_listener);
        ts.Listeners[event_name].TraceOutputOptions = TraceOptions.DateTime | TraceOptions.ProcessId | TraceOptions.Timestamp;
#endif
        ts.Switch.Level = SourceLevels.All;
string testString = "<Test><InnerElement Val=\"1\" /><InnerElement Val=\"Data\"/><AnotherElement>11</AnotherElement></Test>";
        UnescapedXmlDiagnosticData unXData = new UnescapedXmlDiagnosticData(testString);
        ts.TraceData(TraceEventType.Error, 38, unXData);
        ts.TraceEvent(TraceEventType.Error, 38, testString);

        Trace.Listeners.Add(eventschema_listener);
        Trace.Write("test eventschema", "test");
        Trace.Flush();
        ts.Flush();
        ts.Close();
        DisplayProperties(ts, event_name);
        Process.Start("notepad++.exe", "TraceOutput.xml");

        Console.ReadLine();
    }
}

privatestaticvoid DisplayProperties(TraceSource ts, string event_name)
{
    Console.WriteLine("IsThreadSafe? " + ((EventSchemaTraceListener)ts.Listeners[event_name]).IsThreadSafe);
    Console.WriteLine("BufferSize =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).BufferSize);
    Console.WriteLine("MaximumFileSize =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).MaximumFileSize);
    Console.WriteLine("MaximumNumberOfFiles =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).MaximumNumberOfFiles);
    Console.WriteLine("Name =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).Name);
    Console.WriteLine("TraceLogRetentionOption =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).TraceLogRetentionOption);
    Console.WriteLine("TraceOutputOptions =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).TraceOutputOptions);
}

 

A tabela a seguir descreve alguns dos elementos e atributos sempre presentes na saída XML.
Elemento	Observações
Computer	Esse elemento representa o valor da MachineName propriedade

Correlation	Se ActivityID não for especificado, o padrão será um GUID vazio.
Event	Este elemento contém um evento de rastreamento.
EventID	Esse elemento representa a entrada deidparâmetro ().
Level	Esse elemento representa a entrada de parâmetro (o valor eventTypenumérico de). Os valores de parâmetro maiores que 255 são resultado como um nível 8, que representa TraceEventType.Information. Os tipos Criticalde evento ErrorInformationde rastreamento,, Verbose , e são gerados como os níveis 1, 2, 4, 8 e 10, respectivamente. Warning

RenderingInfo	Esse atributo representa uma cadeia de caracteres de recurso para o tipo de evento. É sempre "en-EN\"

Você pode usar a ferramenta Visualizador de rastreamento de serviço (SvcTraceViewer. exe) para exibir os dados do evento.

O Visualizador de Rastreamento de Serviço oferece suporte a três tipos de arquivos:
•	Arquivo de rastreamento do WCF (. svclog Connector)
•	Arquivo de rastreamento de eventos (.etl)
•	Arquivo de rastreamento Crimson

O Visualizador de Rastreamento de Serviço permite que você abra qualquer arquivo de rastreamento com suporte, adicione e integre arquivos de rastreamento adicionais, ou abra e mescle um grupo de arquivos de rastreamento simultaneamente.

Inicie o Visualizador de rastreamento de serviço navegando até o local de instalação do WCF (C:\Arquivos de Programas\microsoft SDKs\Windows\v6.0\Bin) e digite na janela de comando SvcTraceViewer.exe ou clique duas vezes no executável.
 
 

TextWriterTraceListener

Se você não deseja que o DefaultTraceListener esteja ativo, limpe a coleção de listen¬ers atual (Listeners.Clear()). Você pode adicionar quantos listen¬ers quiser(Listeners.Add(textListener). Após a execução desse código, é criado um arquivo de saída chamado Tracefile.txt que contém a saída do rastreamento(File.Create("tracefile.txt")), que será criado na pasta bin >> Debug do projeto.

staticvoid Main(string[] args)
{
    TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);

    //OU Stream file = new FileStream("TraceFile.txt", FileMode.Append);
    Stream outputFile = File.Create("tracefile.txt");
    TextWriterTraceListener textListener = new TextWriterTraceListener(outputFile);
    traceSource.Listeners.Clear();
    traceSource.Listeners.Add(textListener);

    traceSource.TraceInformation("Config_TraceListener application..");
    traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
    traceSource.TraceData(TraceEventType.Information, 1, newobject[] { "a", "b", "c" });
    traceSource.Flush();
    traceSource.Close();
}

 

Você pode definir seus próprios listen¬ers de rastreamento herdando da classe base TraceListener e especificando sua própria implementação para os métodos de rastreamento.


XmlWriterTraceListener

Direciona a saída de rastreamento ou de depuração como dados codificados em XML para um TextWriter ou Stream, como um FileStream.

staticvoid Main(string[] args)
{
string ne_fileName = "NotEscaped.xml";
string es_fileName = "Escaped.xml";
string event_name = "xmlwriter_listener";
string testString = "<Test><InnerElement Val=\"1\" /><InnerElement Val=\"Data\"/><AnotherElement>11</AnotherElement></Test>";

    File.Delete(ne_fileName);
    File.Delete(es_fileName);

//specify the XmlWriter trace listener
using (var xmlwriter_listener = new XmlWriterTraceListener(ne_fileName, event_name))
    {
        TraceSource ts = new TraceSource("TestSource");

        ts.Listeners.Add(xmlwriter_listener);
        ts.Switch.Level = SourceLevels.All;

        XmlTextReader myXml = new XmlTextReader(new StringReader(testString));
        XPathDocument xDoc = new XPathDocument(myXml);
        XPathNavigator myNav = xDoc.CreateNavigator();
        ts.TraceData(TraceEventType.Error, 38, myNav);

        ts.Flush();
        ts.Close();
    }

using (var xmlwriter_listener = new XmlWriterTraceListener(es_fileName, event_name))
    {
        TraceSource ts2 = new TraceSource("TestSource2");
        ts2.Listeners.Add(xmlwriter_listener);
        ts2.Switch.Level = SourceLevels.All;
        ts2.TraceData(TraceEventType.Error, 38, testString);

        ts2.Flush();
        ts2.Close();
    }

    Console.ReadLine();
}

Assim como o EventSchemaTraceListener, você pode usar a ferramenta Visualizador de rastreamento de serviço (SvcTraceViewer. exe) para exibir a saída XML e pode també utilizer da propriedade Filter da classe base TraceListener para adicionar um nível de filtragem de saída de rastreamento no ouvinte. 
 
 
 

App.config 

Especificar os listen¬ers através do código pode ser útil, mas não é algo que você possa alterar facilmente após a implantação do aplicativo. Em vez de configurar os listen¬ers através do código, recomendamos o uso de arquivos de configuração de aplicativo, porque eles permitem adicionar, modificar ou remover ouvintes de rastreamento sem a necessidade de alterar o código.
Se não houver nenhum arquivo app.config:
1.	No menu Projeto, escolha Adicionar Novo Item.
2.	Na caixa de diálogo Adicionar Novo Item, selecione Arquivo de Configuração de Aplicativo.
3.	Clique em Adicionar.

Declare o ouvinte de rastreamento no arquivo de configuração de aplicativo. Se o ouvinte que você está criando exigir outros objetos, declare-os também. O exemplo a seguir mostra como criar um ouvinte chamado myListener que grava no arquivo de texto TextWriterOutput.log.

<configuration>
<system.diagnostics>
<traceautoflush="false"indentsize="4">
<listeners>
<addname="myListener"type="System.Diagnostics.TextWriterTraceListener"
initializeData="TextWriterOutput.log" />
<removename="Default" />
</listeners>
</trace>
</system.diagnostics>
</configuration>

Use a classe Trace no código para gravar uma mensagem nos ouvintes de rastreamento.

staticvoid Main(string[] args)
{
    Trace.TraceInformation("Test Config Listener message.");
    Trace.Flush();
    Console.ReadKey();
}

 

Adicione o ouvinte de rastreamento à coleção Listeners e envie informações de rastreamento para os ouvintes. Repare que o nome myListener é o nome definido no App.config.

staticvoid Main(string[] args)
{
    Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
    Trace.TraceInformation("Test Config Listener LOG message.");
    Trace.Flush();
    Console.ReadKey();
}

 

Se você não desejar que o ouvinte receba a saída de rastreamento, não adicione-a à coleção Listeners. Emita a saída por meio de um ouvinte independente da coleção Listeners chamando os próprios métodos de saída do ouvinte. O exemplo a seguir mostra como gravar uma linha em um ouvinte que não está na coleção Listeners.

staticvoid Main(string[] args)
{
    var myListener = new TextWriterTraceListener("TextWriterOutput.log", "myListener");
    myListener.WriteLine("Test Config SEM ADD Listener message.");
    myListener.Flush();
    Console.ReadKey();
}

	No exemplo exemplo anterior foi utilizado a classe trace para dar a saída dos resultados de nosso teste. Podemos fazer o mesmo com a classe TraceSource onde resultará em um arquivo config com uma combinação bastante complexa de filtros de tipo de evento, juntamente com listeners de rastreamento personalizados, fontes de rastreamento e comutadores de rastreamento. 

<?xmlversion="1.0"encoding="utf-8" ?>
<configuration>
<startup>
<supportedRuntimeversion="v4.0"sku=".NETFramework,Version=v4.6.1" />
</startup>
<system.diagnostics>
<traceautoflush="true"/>
<sources>
<sourcename="ConfigTraceSource"switchName="defaultSwitch">
<listeners>
<addinitializeData="Configtxt_TraceFile.txt"name="configtxt_listener"
traceOutputOptions="DateTime"
type="System.Diagnostics.TextWriterTraceListener">
<filtertype="System.Diagnostics.EventTypeFilter"
initializeData="Error"/>
</add>
<addname="configconsole_listener" />
<addname="configxml_listener" />
<addname="configdelimited_listener" />
<removename="Default"/>
</listeners>
</source>
</sources>
<sharedListeners>
<addinitializeData="configoutput.xml"type="System.Diagnostics.XmlWriterTraceListener"
name="configxml_listener"traceOutputOptions="None" />
<addtype="System.Diagnostics.ConsoleTraceListener"name="configconsole_listener"
traceOutputOptions="None" />
<addtype="System.Diagnostics.DelimitedListTraceListener"
delimiter=","name="configdelimited_listener"
initializeData="delimitedOutput.csv"
traceOutputOptions="ProcessId, DateTime" />
</sharedListeners>
<switches>
<!--<add name="myswitch" value="Verbose" />-->
<addname="defaultSwitch"value="All" />
</switches>
</system.diagnostics>
</configuration>

Você pode fornecer a origem de um ouvinte de rastreamento dentro da tag sources, onde especifica a origem do ouvinte. Essa fonte pode usar dois tipos ouvintes de rastreamento: texto e console.

Primeiro, definimos a propriedade autoflush como true, o que significa que todos os eventos rastreados serão enviados imediatamente aos ouvintes e definimos um ouvinte, o TextWriterTraceListener, que grava as mensagens no arquivo de texto Configtxt_TraceFile.txte anexa a data e a hora a cada mensagem. Em seguida, definimos a fonte de rastreio que corresponde à criada no código pelo nomeConfigTraceSource para usar quatro listeners: o principal em um arquivo, Segundo no console, terceiro em xml e útimo delimited. O listen¬erdo console, xml e definiedsão definidos como listen¬ercompartilhado (shared). Para que você possa usá-lo para várias origens de rastreamento o nome definido em add deve ser o mesmo referenciado em sharedListeners.O ouvinte padrão é removido em <remove name="Default"/>
.
A tag switchNameespecifica o rótulodefaultSwitch de origem no construtor TraceSource. Seu valor especifica como lidar com as mensagens recebidas: "All" significa todos os tipos de mensagens. Você pode especificar que tipo de mensagem pode ser visualizada dessa maneira. O switch funciona no TraceSourcelevel, portanto, seu impacto será sobre todos os ouvintes definidos em uma fonte de rastreamento. 

Para especificar o tipo de mensagem que você pode ver ou determinar qual evento é processado para um ouvinte específico, você pode aplicar um filtro. Enquanto os switches trabalham para uma fonte de rastreamento inteira, um filtro é aplicado em um listen¬erindividual. Quando você tem vários ouvintes para uma única fonte de rastreamento, pode usar filtros para determinar quais eventos de rastreamento são realmente processados pelo listen¬er. 

Para cada ouvinte, podemos definir o EventTypeFilter. Por exemplo, se queremos que apenas erros sejam gravados no log de eventos do sistema e todos sejam rastreados para o arquivo de texto, podemos definir um filtro de tipo de evento com initializeData = "Error" no ouvinte do log de eventos, para que as únicas mensagens de erro sejam exibidas. ser aceito por este ouvinte.Os valores possíveis para a opção de rastreio são definidos em detalhes aqui, mas os mais comuns são: Erro, Aviso, Informações e Detalhado (Verbose).

	Para facitar a criação e edição dos listeners no arquico config é possívelo acessar a interface gráfico pela opção “Editar Configurações de WCF”.
  

classProgram
{
//Criado um TraceSource pelo nome definido no App.config
publicstatic TraceSource traceSource = new TraceSource("ConfigTraceSource");

staticvoid Main(string[] args)
    {
        traceSource.TraceEvent(TraceEventType.Warning, 0, "Some strange warning message");
        traceSource.TraceInformation("info message");
        traceSource.TraceEvent(TraceEventType.Error, 0, "Fatal error occured");
        traceSource.TraceEvent(TraceEventType.Verbose, 0, "Some debug message");
    }
}

 

PROFILING

Profiling é o processo de determinar como seu aplicativo usa certos recursos. Você pode verificar, por exemplo, quanta memória seu programa usa, quais métodos estão sendo chamados e quanto tempo cada método leva para executar. Essas informações são necessárias quando você tem um gargalo de desempenho e deseja encontrar a causa.

Na maioria das vezes, o desempenho é visto como a quantidade de tempo que algo leva. Esse não é o único critério de desempenho, no entanto. Em relação a desempenho, uma coisa é sempre verdadeira: não entre em otimizações prematuras, como tentarmelhorar o algoritmo para ficar mais rápido, sem ao menos saber se esse algoritmo é o gargalo de seu aplicativo. 

Existem duas maneiras principais de criar perfis de um aplicativo:
1.	Manualmente (StopWatch) ou Contador de desempenho
2.	Usando a ferramenta Assistente de Desempenho do Visual Studio

Manualmente(StopWatch)

Quando você enfrenta problemas de desempenho, pode usar um profiler para realmente medir qual parte do seu aplicativo está causando problemas.A criação de perfil manual é instrumentar, ou seja, inserir algum código para assistir ao desempenho de um aplicativo.

Uma maneira simples de medir o tempo de execução de algum código é usando a classe Stopwatch, que pode ser encontrada no espaço para nome System.Diagnostics. 

classProgram
{
constint numberOfIterations = 100000;
staticvoid Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Console.WriteLine("Inicio: " + sw.Elapsed);
        Algorithm1();
        sw.Stop();
        Console.WriteLine("Fim Algorithm1: " + sw.Elapsed);
        sw.Reset();
        sw.Start();
        Console.WriteLine("Inicio 2: " + sw.Elapsed);
        Algorithm2();
        sw.Stop();
        Console.WriteLine("Fim Algorithm2: " + sw.Elapsed);
        Console.ReadLine();
    }

privatestaticvoid Algorithm1()
    {
        StringBuilder sb = new StringBuilder();
for (int x = 0; x < numberOfIterations; x++)
        {
            sb.Append('a');
        }
string result = sb.ToString();
    }

privatestaticvoid Algorithm2()
    {
string result = "";
for (int x = 0; x < numberOfIterations; x++)
        {
            result += 'a';
        }
    }
}
 

Como você pode ver, a classe StopWatch possui um método Start, Stop e Reset. Você pode obter o tempo decorrido em milissegundos, em ticks ou formatado como no exemplo.

Performance Counter

O Windows fornece um grande número de contadores de desempenho categorizados que você pode usar para monitorar seu hardware, serviços, aplicativos e drivers. Exemplos de contadores de desempenho são aqueles que exibem o uso da CPU e o uso da memória, mas também contadores específicos de aplicativos, como o tamanho de uma consulta no SQL Server.

Contador de desempenho é outra abordagem do Profiling manual que permite monitorar o desempenho de um aplicativo. Esses contadores de desempenho são gerenciados pelo Windows como o EventLogs e você pode visualizá-los usando o programa Perfmon.exe (Monitor de Desempenho).
 

Você pode ler os valores dos contadores de desempenho do código usando a classe PerformanceCounter encontrada no espaço para nome System.Diagnostics. O exemplo abaixo mostra a utilização de um contador de desempenho para ler a quantidade de memória disponível e exibi-la na tela.

staticvoid Main(string[] args)
{
    Console.WriteLine("Press escape key to stop");
using (PerformanceCounter pc = new PerformanceCounter("Memory", "Available Bytes"))
    {
string text = "Available memory: ";
        Console.Write(text);
do
        {
while (!Console.KeyAvailable)
            {
                Console.Write(pc.RawValue);
                Console.SetCursorPosition(text.Length, Console.CursorTop);
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }
}

Existem duas maneiras de criar um contador de desempenho.
1.	Usando código
2.	Usando o Server Explorer

Para criar um o Contador de Desempenho usando código utilize a classe PerformanceCounterCategory e PerformanceCounter para interagir com eles. Você também pode criar contadores de desempenho manuais usando o Server-Explorer. Para acessar os contadores de desempenho, seu aplicativo precisa ser executado com permissão de um administrador ou fazer parte do grupo Usuários do Monitor de Desempenho.
staticvoid Main(string[] args)
{
if (!PerformanceCounterCategory.Exists("ShoppingCounter"))
    {
        CounterCreationDataCollection counters = new CounterCreationDataCollection();
// *1.contador para contar totais(ShoppingDone): PerformanceCounterType.NumberOfItems32 * /
        CounterCreationData totalDone = new CounterCreationData();
        totalDone.CounterName = "ShoppingDone";
        totalDone.CounterHelp = "Número total de compras concluídas";
        totalDone.CounterType = PerformanceCounterType.NumberOfItems32;
        counters.Add(totalDone);

// 2.contador para contar totais(ShoppingNotDone): PerformanceCounterType.NumberOfItems32
        CounterCreationData totalNotDone = new CounterCreationData();
        totalNotDone.CounterName = "ShoppingNotDone";
        totalNotDone.CounterHelp = "Número total de compras não concluídas";
        totalNotDone.CounterType = PerformanceCounterType.NumberOfItems32;
        counters.Add(totalNotDone);

// cria uma nova categoria com os contadores acima
var texto = "Os balcões de compras ajudam a montar quantas compras são feitas e como muitos não são.";
        PerformanceCounterCategory.Create("ShoppingCounter", texto, counters);
        Console.WriteLine("Contador de desempenho criado.");
    }
else
        Console.WriteLine("Contador de desempenho já criado.");

    Console.ReadKey();
}

Todos os contadores de desempenho fazem parte de uma categoria e, dentro dessa categoria, têm um nome exclusivo. PerformanceCounterCategory classe é usada para criar os contadores de desempenho para uma categoria específica. Também é usado para excluir o contador de desempenho ou verificar a existência deles. Os contadores de desempenho são definidos para uma categoria específica usando a classe CounterCreationData. Como é preciso criar uma lista de contadores o CounterCreationDataCollection agrupa todas as instâncias da classe CounterCreationData.

O código acima criará a categoria de desempenho "ShoppingCounter" e adicionará dois contadores,"ShoppingDone" e "ShoppingNotDone", nele. Você pode visualizá-los navegando para Server Explorer.

Os contadores de desempenho vêm em vários tipos diferentes. A definição de tipo determina como o contador interage com os aplicativos de monitoramento. Alguns tipos que podem ser úteis são:
•	NumberOfItems32/NumberOfItems64: Esses tipos podem ser usados para contar as número de operações ou itens. NumberOfItems64 é o mesmo que NumberOfItems32, exceto que ele usa um campo maior para acomodar valores maiores.
•	RateOfCountsPerSecond32/RateOfCountsPerSecond64: Esses tipos podem ser usados para calcular a quantidade por segundo de um item ou operação. RateOfCountsPerSecond64 é o mesmo que RateOfCountsPerSecond32, exceto que ele usa campos maiores para acomodar para valores maiores.
•	AvergateTimer32: Calcula o tempo médio para executar um processo ou um processo item.

No exemplo abaixo mostra mais um exemplo de criação e uso de seus próprios contadores de desempenho. Na primeira vez, o aplicativo criará dois novos contadores de desempenho. Na segunda vez, ele aumentará os dois contadores em uma unidade. Se você executar este programa (como administrador) e ficar de olho na ferramenta Monitoramento de Desempenho do Windows, verá os dois contadores atualizados.

staticvoid Main2(string[] args)
{
if (CreatePerformanceCounters())
    {
        Console.WriteLine("Created performance counters");
        Console.WriteLine("Please restart application");
        Console.ReadKey();
return;
    }

var totalOperationsCounter = new PerformanceCounter(
"MyCategory", "# operations executed", "", false);
var operationsPerSecondCounter = new PerformanceCounter(
"MyCategory", "# operations / sec", "", false);

    totalOperationsCounter.Increment();
    operationsPerSecondCounter.Increment();
}

privatestaticbool CreatePerformanceCounters()
{
if (!PerformanceCounterCategory.Exists("MyCategory"))
    {
        CounterCreationDataCollection counters = new CounterCreationDataCollection
        {
new CounterCreationData(
"# operations executed",
"Total number of operations executed",
                PerformanceCounterType.NumberOfItems32),
new CounterCreationData(
"# operations / sec",
"Number of operations executed per second",
                PerformanceCounterType.RateOfCountsPerSecond32)
        };
        PerformanceCounterCategory.Create(
"MyCategory", "Sample category for Codeproject",
            PerformanceCounterCategoryType.SingleInstance,
            counters);
returntrue;
    }

returnfalse;

}

Server Explorer

O Server Explorer é uma ferramenta que está disponível no IDE do .NET e que permite o acesso a diversos recursos. Com ele podemos realizar pesquisas em vários servidores de uma rede visualizando os serviços que estão em execução. Você também pode usar o Server Explorer para acessar os serviços e facilitar sua vida.
 

O Server Explorer apresenta uma lista hierárquica dos serviços disponíveis no seu computador (ou computador selecionado se você estiver em rede). Os serviços disponíveis podem se classificados em duas categorias:
•	Conexões de Dados - Permite conectar e visualizar os bancos de dados no ambiente do.NET
•	Servidores - Exibe a lista de servidores com os quais você esta conectado e os serviços em execução.
 

Clique com o botão direito do mouse no “ShoppingCounter” e clique em Editar. Você também pode adicionar novos contadores dessa maneira ou editar ou visualizá-los. Os detalhes são parecidos com:
 

Após a criação do contador de desempenho, eles devem ser usados para monitorar o desempenho de um aplicativo.

staticvoid Main(string[] args)
{
int noOfShoppingsDone = 1500;
int noOfShoppingsNotDone = 20000;

// successfully Done shpping (Counter)
using (PerformanceCounter successfullCounter = new PerformanceCounter("ShoppingCounter", "ShoppingDone"))
    {
        successfullCounter.MachineName = ".";
        successfullCounter.ReadOnly = false;

for (int i = 0; i < noOfShoppingsDone; i++)
        {
            Console.WriteLine("Shopping Done Successfully..");
            successfullCounter.Increment();
        }
    }

using (PerformanceCounter NotsuccessfullCounter = new PerformanceCounter())
    {
        NotsuccessfullCounter.CategoryName = "ShoppingCounter";
        NotsuccessfullCounter.CounterName = "ShoppingNotDone";
        NotsuccessfullCounter.MachineName = ".";
        NotsuccessfullCounter.ReadOnly = false;

for (int i = 0; i < noOfShoppingsNotDone; i++)
        {
            Console.WriteLine("Shoppings Not Done..");
            NotsuccessfullCounter.Increment();
        }
    }

//Console.ReadKey();
}

Em algum lugar do seu aplicativo, você faz com que seu contador seja bem-sucedido e mal-sucedido com base na lógica (ou seja, onde as compras não seriam possíveis ou possíveis). Para fazer isso, você precisa inicializar seu contadores de desempenho e use-os com as funções fornecidas:
1.	Increment(): Incremente o valor do contador em 1.
2.	IncrementBy(): Incremente o valor do contador com o valor fornecido.
3.	Decrement(): Reduza o valor do contador por 1.
4.	DecrementBy(): Decrescente o valor do contador com o valor fornecido.

Você pode visualizar suas alterações ou monitorar esses contadores de desempenho acessando o Monitor de Desempenho. Para visualizar o Monitor de Desempenho, pressione o comando “Executar” pressionando a tecla janela + R, digite perfmon e pressione enter. Selecione Monitor de desempenho no lado esquerdo do menu. Clique no botão verde mais (no topo). 
 

Uma janela de "Adicionar contadores" aparecerá na sua frente. Pesquise na sua categoria de contador >> clique nele e clique no botão Adicionar. Isso adicionará o contador selecionado no painel “Added Counter” do mesmo janela (você pode adicionar quantos contadores quiser). Clique no botão "OK":
 

Depois de clicar no botão OK, o desempenho dos contadores selecionados é exibido no Contador de desempenho. Dessa forma, você pode monitorar a saúde do seu aplicativo.
 

 

Criar seus próprios contadores de desempenho pode ser uma grande ajuda ao verificar a integridade do seu aplicativo. Você pode criar outro aplicativo para lê-los (algum tipo de aplicativo de painel) ou pode usar a ferramenta Monitor de Contador de Desempenho fornecida pelo Windows.

Ferramenta Assistente de Desempenho do Visual Studio

O Visual Studio também inclui um amplo conjunto de ferramentas de criação de perfil. Para usá-los, você precisa do Visual Studio Ultimate, Premium ou Professional edition.Ao usar o criador de perfil, a maneira mais fácil é usar o Assistente de Desempenho. Você pode encontrar esse assistente no menu Analyze no Visual Studio. A Figura a seguir mostra a primeira página do assistente.
 

Ao criar um perfil de seus aplicativos, você tem quatro opções:
1.	CPU sampling (Amostragem): Esta é a opção mais leve. Tem pouco efeito sobre a aplicação. Você o utiliza para uma pesquisa inicial dos seus problemas de desempenho.
2.	Instrumentation: Esse método injeta código no arquivo compilado que captura informações de tempo para cada função chamada. Com a instrumentação, você pode encontrar problemas relacionados à entrada/saída (E/S) ou pode examinar atentamente um método específico.
3.	.NET memory allocation: Esse método interrompe seu programa toda vez que o aplicativo aloca um novo objeto ou quando o objeto é coletado pelo coletor de lixo para fornecer uma boa idéia de como a memória está sendo usada no seu programa.
4.	Resource contention data: Esse método pode ser usado em aplicativos multithread para descobrir por que os métodos precisam esperar um pelo outro antes de acessar um recurso compartilhado.

Se você executar o aplicativo da Lista 3-49 sem o código do cronômetro e criar um perfil com a opção CPU Sampling, verá um relatório parecido com o da figura a seguir.
 

