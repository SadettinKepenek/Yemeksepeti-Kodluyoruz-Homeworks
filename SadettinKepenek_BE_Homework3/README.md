# C# Basics

## Indexer
Indexers bir nesnenin dizi gibi indekslenmesini sağlar. Tıpkı bir array tanımladığımız da bu array içinde hangi indeksteki elemanı görmek istiyorsak çağırdığımız gibi, indexer'lar da Class'ımızın bir array gibi olmasını sağlamaktadır.

``` c-sharp
public class Person  
{  
    private string[] persons = {"John Doe","Jon Snow","Tryion Lannister"};  
      
    //indexer  
  public string this[int index]  
    {  
        get => persons[index];  
        set => persons[index] = value;  
    }  
}

```
Bu örnekte bir Person sınıfı oluşturduk ve içinde string dizisinde bir persons array'i tanımladık. 
``` c-sharp
static void Main(string[] args)  
{  
    Person p = new Person();  
    var firtPerson = p[0];  
    Console.WriteLine(firtPerson);  
}
```
Person nesnesinin bir array gibi kullanıp verdiğimiz index'deki değeri getirtmiş olduk.

## Değer ve Referans Tipleri
C# ta kullanmakta olduğumuz değişken tipleri ikiye ayrılmaktadır:Değer ve Referans tipleri.
Değer Tipleri stack bölgesinde tutulurken, Referans Tipleri heap bölgesinde tutulmaktadır.

**Değer Tipleri:** “int”, “long”, “float”, “double”, “decimal”, “char”, “bool”, “byte”, “short”, “struct”, “enum”  
**Referans Tipleri:** “string”, “object”, “class”, “interface”, “array”, “delegate”, “pointer”

Bir metod içerisinde oluşturulan değişkenler ve metod parametreleri stack bellek bölgesinde oluşturulur ve metod sonlandığında bellekten silinirler.Bir metoda yolladığımız değer tipinde ki parametreler üzerinde ne kadar işlem yaparsak yapalım değerleri değişmez.Örnek verecek olursam:

```c-sharp
 static void Main(string[] args)  
    {  
        int toplam = 0;  
        Add(5,toplam);  
        Console.WriteLine(toplam);  
    }  
  
    public static void Add(int a,int toplam)  
    {  
        toplam += a;  
    }   
}
```

Bu kodun çıktısı 0 olacaktır.Çünkü toplam değişkleni bir değer değişkenidir ve metoda parametre olarak sadece değeri gönderilir.

```c-sharp
static void Main(string[] args)  
{  
    int toplam = 0;  
    AddWithRef(5,ref toplam);  
    Console.WriteLine(toplam);  
}  
public static void AddWithRef(int a, ref int toplam)  
{  
    toplam += a;  
}
```

**ref** anahtar kelimesiyle değer tipininin referansını parametre olarak yolladık ve bu sayede gerçekten değerini değiştirmiş olduk.

## Delegate
C#'da delegate(Temsilci),tanımlanan metodların bellekteki adreslerini tutan temsilcilerdir.C++'daki fonksiyon göstericilerine benzerler.

```c-sharp
delegate void NumberDelegate(int a,int b);
public static void Main(string[] args)  
{  
  
    NumberDelegate delegateMethod1 = Add;  
    delegateMethod1.Invoke(3,2);  
    delegateMethod1(5, 4);  
      
  
    NumberDelegate delegateMethod2 = (a, b) => Multiply(a,b);  
    delegateMethod2.Invoke(3,4);  
  
}  
  
public static void Add(int a,int b)  
{  
    Console.WriteLine($"{a} + {b} = {a+b}");  
}  
public static void Multiply(int a,int b)  
{  
    Console.WriteLine($"{a} * {b} = {a*b}");  
  
}
```

Delegate olarak tanımladığımız yapı,bir fonksiyonu işaret etmelidir.İşaret edilen fonksiyonu  delegateMethod(a,b) ya da delegateMethod.Invoke(a,b) kullanılarak çağırılır.

## Act,Func ve Predicate
C#'da  Action dönüş tipi olmayan (void), Func dönüş tipi generic olan, Predicate ise dönüş tipi bool olan özel delege yapılarıdır. 16'ya kadar parametre alabilirler.

### Action
```c-sharp
static void Main(string[] args)  
{  
    Action<string, string> act = PrintFullName;  
    act("John","Doe");  
}  
  
static void PrintFullName(string firstName,string lastName)  
{  
    Console.WriteLine($"{firstName} {lastName}");  
}
```
Action'un işaret ettiği fonksiyon string tipinde iki tane parametre alıyor.Bu yüzden Action'u Action<string,string> şeklinde tanımladık.

### Func
```c-sharp
static void Main(string[] args)  
{   
    Func<string,string> func = ReplaceSpaces;  
    var result = func(" Test  ");  
    Console.WriteLine(result);  
}
static string ReplaceSpaces(string str)  
{  
    return str.Replace(" ", "");  
}
``` 
Func tipindeki delegate'ler değer döndüren fonksiyonlar için kullanılır.Bu örnekte delegate'imizi Func<string,string> şeklinde tanımladır.İlk string,işaret ettiğimiz metodunun paremetresi,ikinci string ise işaret ettiğimiz metodun dönüş tipidir.

### Predicate
```c-sharp
static void Main(string[] args)  
{  
    Predicate<string> isUpper = IsUpperCase;  
    var predicateResult = isUpper("TEST");  
    Console.WriteLine($"Is String UpperCase ? : {predicateResult}");     
}
static bool IsUpperCase(string str)  
{  
    return str.Equals(str.ToUpper());  
}
````
Predicate tipindeki delegate'ler,boolean tipinde değer döndüren fonksiyonlar için kullanılır.Örnekte bir stringin tüm harfleri büyük mü değil mi kontrolünü yapan IsUpperCase metodumuz var.Predicate<string> delegate'i ile metodumuzu işaret ediyoruz ve çalıştırıyoruz.İşaret ettiğimiz metod sadece bir tane string parametresi aldığı için Predicate'i tanımlarken 1 tane string ibaresi yazdık.

> Func'da dönüş tipi bilinmediğinden dönüş tipinide belirtmemiz gerekiyordu fakat Predicate'de dönüş tipi bool olduğundan dönüş tipini berlitmemize gerek yok.

## Dynamic
C#'da bildiğimiz üzere object tipinde tanımladığımız değişkenlere her tipte değeri atayabiliriz.Object tipindeki değişkenlerin değerleri derleme zamanında atanır.

```c-sharp
static void Main(string[] args)  
{  
    object a = 10;  
    Console.WriteLine(a.GetType());  
      
    //Aşağıdaki kod hata verir  
    // a = a + 5;
    }
```
Bu kodun çıktısı System.Int32 olur fakat hala derleyici a değişkeninin tipini object olarak biliyor.O yüzden a = a + 5 işlemi hata verir.

Bu gibi durumlarda değişkenin tipini çalışma zamanında atamak için **dynamic** tipini kullanırız.

```c-sharp
static void Main(string[] args)  
{  
    dynamic b = 10;  
    Console.WriteLine(b.GetType());  
    b = b + 5;  
    Console.WriteLine(b);  
}
```
b değişkeninin tipi System.Int32 ve değeri 15 olur.Çünkü çalışma zamanında değişkenin değerini değiştirebiliyoruz;

## Partial Class
Partial class bize bir class' ı birden fazla class olarak bölmemize, constructor, değişken, property, metodları vs. düzenli bir şekilde ayrı ayrı oluşturmamızı sağlamaktadır. Fiziksel olarak birden fazla parça ile oluşan partial class' lar, çalışma zamanında tek bir class olarak bütün elemanları içerisinde barındırmaktadır.

Örnek olarak bir Emplooye sınıfı oluşturalım.
```c-sharp
public partial class Employee  
{  
    public string Firstname { get; set; }  
    public string Lastname { get; set; }  
  
    public Employee(string firstname, string lastname)  
    {  
        Firstname = firstname;  
        Lastname = lastname;  
    }  
}
```
Sadece property ve constructer'ı tanımladıktan sonra metodlarını ayrı bir dosyada yazalım.

```c-sharp
public partial class Employee  
{  
    public string GetFullName()  
    {  
        return $"{Firstname} {Lastname}";  
    }  
}
```

Gördüğünüz gibi ayrı dosyalarda Employee sınıfını birleştirebiliyoruz.

```c-sharp
static void Main(string[] args)  
{  
    Employee employee = new Employee("John", "Doe");  
    var fullName = employee.GetFullName();  
    Console.WriteLine(fullName);  
}
```
Büyük projelerde kodumuz okunması zor bir hale geldiğinde partial class'lara bölerek daha anlaşılır hale getirebilririz.
