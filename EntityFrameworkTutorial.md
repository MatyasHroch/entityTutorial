## Úvod
- entity budou vždy
- jak co nejjednodušeji nadefinovat entity a jejich vazby? (někdy vazby nejsou potřeba)
- proč abstrakce?
	- lépe se s tím dělá, když se změny promítají samy do databáze, podle toho, jak jsou objekty změněny
	- prakticky žádné SQL apřitom využíváme jeho sílu
	- možnost změny jedné či druhé technologie
	- optimalizace, dokud se nedá ‘save’, neprovádí se dotaz
	- možnost mít i více databází
- vazby jsou problém, jaké vazby máme? → 1:1, 1:N, N:M
- kam co napsat, aby to fungovalo tak, jak bychom očekávali?
## 1) Vytvoříme třídy
- [ ] musí **mít slot Id** může být i ‘nazevTridy’+Id
- [ ] musí mít jeden **kostruktor prázdný**
- [ ] pokud mají sloty typu dalších tříd anebo kolekcí těchto tříd, tak musí být označeny **virtual**
- proč to všechno? 
	- → protože to jinak nefunguje dobře
	- (virtual je kvůli proxies, Id je, aby třída odpovídala tabulce, prázdný konstruktor je kvůli reflexe)
#### 1:1
- slot pro jeden objekt na jedné straně
- Country → CapitalCity
- CapitalCity → Country
- jak v SQL tabulkách?
	- countryId u CapitalCity, popřípadě i cityId u Country
#### 1:N
- slot pro jeden objekt na jedné straně a slot pro kolekci objektů na druhé
- Author (1) → Books (N)
- Books (N) → Author (1)
- jak v SQL tabulkách?
	- jen authorId u Books
#### M:N
- slot pro kolekci objektů na obou stranách
- pokud obsahuje seznam entit, tak to nadefinujeme jako ICollection<\T>
- Students (N) → Courses (M)
-  Courses (N) → Students (M)
- jak v SQL tabulkách?
	- nová tabulka s oběma ID
## Pohled do Databáze
- pro přehled bychom si měli stáhnout 
	- nějaký prohlížeč sqLite databáze, např. https://sqlitebrowser.org/dl/
	nebo
	- doplněk do VS Code → https://marketplace.visualstudio.com/items?itemName=qwtel.sqlite-viewer
## Constrains
- Pokud to nepovoluje třída mít nějaký slot NULL, tak ani tabulka
- Pokud má třída defaultní hodnotu, tak i v tabule bude defaultní hodnota
## 2) Nuget balíčky
- je nutné stáhnout 4 balíčky přes Nuget přímo ve Visual Studiu
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.Design 
		- → pro migrace
	- Microsoft.EntityFrameworkCore.Proxies 
		- → pro líné vyhodnocování
	- Microsoft.EntityFrameworkCore.Sqlite 
		- → pro SqLite databázi
- po stáhnutí je občas nutný restart Visual Studia
## 3) Vytvoření Context třídy
- vytvoříme speciální třídu pojmenovanou se suffixem ‘Context’ 
	- ta bude obsahovat všechny třídy, které chceme přes Entity Framework prohnat, aby nám vytvořil ty správné tabulky a vazby
- nahoru dáme **using Microsoft.EntityFrameworkCore**
- ta Context třída musí:
	- dědit z **DbContext**
	- mít **prázdný kostruktor**
- *ukázka samotného Contextu:*
` internal class UpolnicekContext : DbContext
{

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }

    public UpolnicekContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    {
        options.UseLazyLoadingProxies().UseSqlite(@"Data Source=realPathToYourDatabase"); 
    }
}`
## 4) Čachry machry s komand lajnou
- před tímto krokem je často nutné vypnout a zapnout Visual Studio (i 2022)
- přes ‘nástroje’ → ‘správce balíčků Nuget’ (‘tools’ → Nuget Manager) se dostaneme do příkazové řádky
	- na začátek dáme tyto příkazy:
	- `dotnet tool install --global dotnet-ef` 
	- `cd yourProject` → posunout se do složky projektu
	- `dotnet ef migrations add InitialDb` → vytvoří se migrace databáze
	- `dotnet ef database update` → všechny migrace, které ještě nebyly aplikovány se postupně aplikují.. složí se dokupy a změní databázi
	- kdykoliv cokoliv ve třídách změníme a chceme, aby se změna propsala i do databáze, tak dáme znovu
		- `dotnet ef migrations add RealDbName
		- `dotnet ef database update`
## Migrace
- po každém `dotnet ef migrations add RealDbName` se vygeneruje kód, který databázi přemění na novou verzi a také kód, který změnu odstraňuje
- po každém `dotnet ef database update` se tento kód provede
	- můžou vzniknout problémy → při přidávání většinou ne, to je nejčastější důvod pro změnu v databázi

- POZOR NA ZMĚNU TOHO, CO MŮŽE A NEMŮŽE BÝT NULL
- vždycky se dá vše začít od začátku takto: https://stackoverflow.com/questions/11679385/reset-entity-framework-migrations
## Ukázky použití spolu s Linq
- CRUD
## Callbacky v Entity Frameworku?
- chtěli bychom určitě, aby se dala napsat metoda, která se zavolá vždy, pokud je daný záznam odstraněn, změněn, přidán, … co si o tom myslíte?
- v C# se toto dá řešit dvěma způsoby (kdyžtak mě opravte/doplňte):
	- zkrátka pomocí Controllerů
	- overridenout SaveChanges metodu → to je jediná možnost, ale to znamená, že se to zavolá po každém uložení, ne jen když je například záznam odstraněn, ale možné to je…

#### Přepsání SaveChanges
Nejdřív zavedeme základní třídu nebo společné rozhraní pro entity, které chceme sledovat pro DateTimeUpdated:

```csharp
public abstract class EditableEntityBase
{
    public DateTime DateTimeUpdated { get; internal set; } 
}
```

Entity, které chcete sledovat, by měly tuto třídu rozšířit nebo implementovat rozhraní, které bude vlastnost zpřístupňovat.

Poté ve třídě DbContext přepíšeme metodu `SaveChanges` a vložíme:
```csharp
var updatedEntities = ChangeTracker.Entries()
    .Where(x => x.State == EntityState.Modified)
    .Select(x => x.Entity)
    .OfType<EditableEntityBase>();
foreach (var entity in updatedEntities)
{
    entity.DateTimeUpdated = DateTime.Now; // or DateTime.UtcNow
}

return base.SaveChanges();
```

Můžeme také zahrnout `x.State == EntityState.Added` pro nové záznamy, ale pokud jde o výchozí hodnoty, lepší je spoléhat na výchozí hodnotu než na přepsání SaveChanges.
## Ukázka navíc:
- nechceme mít k dispozici ten kontrétní objekt, ale chceme, aby záznam souvisel přes idecko:
```csharp
public class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; } // Required foreign key property
    public Blog Blog { get; set; } = null!; // Required reference navigation to principal
}
```
- pokud nechceme BLOG Blog, tak musíme nadefinovat explicitně
- když chceme, aby tam bylo ID necháme tam jenom ID
```csharp
{
    public int Id { get; set; }
    public int BlogId { get; set; } // Required foreign key property
}
```
- v Contextu pak ale musíme ručně nastavit vztah, protože teď Entity neví, že to chceme provázat:
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder) 
{ 
 modelBuilder.Entity<Blog>()
 .HasMany(e => e.Posts)
 .WithOne() .HasForeignKey("BlogId")
 .IsRequired(); 
}
```
