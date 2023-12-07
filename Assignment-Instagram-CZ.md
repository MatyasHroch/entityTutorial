# Pomocí Entity Framework

## 1) Vytvořit a propojit následující entity
### Users
- Id
- firstName
- lastName
- age
- fallowing
- fallowed

### Posts
- Id
- text
- imgPath
- tags
- likes
- releaseDate

### Comments
- Id
- author
- likes

### User (1) → Posts (N)
### Post (1) → Comments (N)

## 2) Vyřešit, jak uchovat informace, které souvisí s právě jedním příspěvkem a právě jedním uživatelem
- když někdo dá srdíčko na příspěvek na Instagramu, tak se mu příspěvek ukazuje se srdíčkem, jen tomu, kdo jej označil → právě tyto informace je potřeba nějak uchovat, vymyslete jak, protože bude třeba určit který uživatel dal srdíčko kterému příspěvku
## 3) Vytvořit řešení pro vlákno zpráv mezi dvěma uživateli
- Tak abyste byli schopni
	- přidávat zprávy oběma uživateli
	- vypsat zprávy, co si mezi sebou uživatelé pošlou
	- určit, který uživatel poslal kterou zprávu

## 4) Vytvořit nějaká data a poté vypsat:
- [ ] Všechny příspěvky nějakého uživatele, které jsou starší než týden a v textu neobsahujou slovo “já” ve všech možných podobách (stačí třeba velká i malá písmena)
- [ ] Všechny příspěvky, které určitý uživatel ‘likenul’
- [ ] Všechny příspěvky, které ‘likenuli’ dva různí uživatelé
- [ ] Všechny příspěvky všech uživatelů, které 
	- určitý uživatel sleduje
	- jsou staré maximálně 3 dny
	- seřazené od nejnovějšího po nejstarší
   - (to je v podstatě hlavní obrazovka Instagramu)
   - to vše znamená, že si musíte vytvořit i fieldy pro sledující a sledované

## Rady
1) když chcete prostě jenom jeden záznam jakýkoliv, tak stačí dát 
	```csharp
	var item = ctx.TableName.First()
	```
	- pozor, hází výjimku, když v tabulce není ani jeden záznam!
2) nezapomínejte, že výsledek z Linq musíte většinou převést na ToList() nebo podobnou kolekci
3) doporučuju na vytvoření dummy dat použít nějaký nástroj typu ChatGPT, protože naplnit si databázi testovacími daty bývá otravné
4) rovnou si projekt strukturujte na Třídy a Kontrolery, abyste pak měli větší abstrakci a kontrolu
