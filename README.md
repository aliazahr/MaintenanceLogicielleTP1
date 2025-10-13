# Bienvenue à **SchoolManagerApp**

## Comment exécuter l'application
1. Ouvre un **terminal**.
2. **Navigue** jusqu'au dossier du projet avec la commande:
   ```bash
   cd path/to/SchoolManagerApp
   ```
4. **Exécute** la commande suivante pour lancer l'application:
   ```bash
   dotnet run
   ```
5.  **Exécute** la commande suivante pour implémenter les variables d'environnement dynamiques (vous pouvez mettre les valeurs de votre choix):
   ```bash
   $env:APP_NetworkDelay__MinMs = "800"
   $env:APP_NetworkDelay__MaxMs = "800"
   $env:APP_Salaries__TeacherIncome = "30000"
   $env:APP_Salaries__ReceptionistIncome = "12000"
   $env:APP_Salaries__PrincipalIncome = "60000"
   ```
6. Pour les variables d'environnements statiques, il est possible de les modifier dans le fichier appsettings.json 
---

## Changements effectués
### 1. Option pour **annuler une opération (_Cancel_)**
Lorsqu'on sélectionne une action (ex: _Add_), il est maintenant possible d'**annuler l'opération** avant de choisir un membre à ajouter.
Cette fonctionnalité rend le programme plus intuitive si l'utilisateur change d'avis ou sélectionne par erreur.

---

### 2. Option pour **annuler la dernière action (_Undo last action_)**
Il est désormais possible de **revenir sur la dernière action effectuée**.
Par exemple, si tu ajoutes un étudiant, tu peux sélectionner "_Undo last action_" pour le supprimer.

La fonction garde en mémoire toutes les actions effectuées.
Si tu as ajouté un étudiant, payé le principal et envoyé une plainte, tu peux faire "Undo last action" trois fois pour les annuler dans l'ordre inverse (dernière ajouter au premier ajouté)

Cette fonction rend le programme plus flexible pour corriger une erreur.

---

### 3. Option pour **terminer l'application (_Exit_)**
Une option dans le menu principal permettant de **quitter le programme** proprement est maintenant disponible.
Cela offre à l'utilisateur un moyen clair et rapide d'arrêter l'application.

---

### 4. Conversion du **_network delay_** en **asynchrone**
Le _network delay_ a été converti en fonction **asynchrone** afin d'améliorer les performances lorsque plusieurs paiements sont effectués simultanément.
Ainsi, comme plusieurs professeurs peuvent être payés en même temps, le programme affiche désormais les paiements les uns après les autres dans un délai total de 5 secondes, au lieu d'allonger ce délai pour chaque paiement.

---

### 5. SchoolMember
Smells: Primitive Obsession, Public Fields, Missing Validation, Inconsistent Naming.
Avant : Le téléphone était un int, l’adresse était une string, certains champs étaient publics et il n’y avait presque pas de vérifications.
Après : Le téléphone est un string, l’adresse est un objet Address, on utilise des propriétés privées et on valide nom, adresse et téléphone.

---

### 6. Student
Smells : Low Cohesion, Missing Validation.
Avant : Il y avait une méthode Display() verbeuse, la moyenne était calculée dans Student, et la note n’était pas bornée.
Après : On utilise ToString() concis, on déplace la moyenne dans StudentManager, et Grade devient nullable et bornée entre 0 et 100.

--- 

### 7. StudentManager
Smells : Feature Envy.
Avant : La méthode pour faire la moyenne des étudiants se trouvait dans Student. Comme elle est spécifique à plusieurs étudiants, et non à l’étudiant seulement, ce n’est pas la responsabilité de la classe Student de faire ça.
Après : Création de la classe StudentManager qui s’occupe d’implémenter et de gérer la logique des notes des étudiants.

---

### 8. Principal
Smells : Duplicate Constructors, Magic Numbers.
Avant : Il y avait des constructeurs en double, un revenu par défaut codé en dur et un Display().
Après : Il reste un seul constructeur qui appelle la base, DefaultIncome devient une constante, on passe à ToString() et on valide que le revenu n’est pas négatif.

--- 

### 9. Teacher
Smells : Missing Validation, Magic Numbers, Encapsulation manquante.
Avant : Aucune vérification, la méthode Display() était utilisée au lieu de ToString(). Les méthodes commençaient par des minuscules. La méthode Display() écrivait une chaîne beaucoup trop longue et risquée.
Après : DefaultIncome est une constante, revenu ≥ 0, et on passe par des propriétés. Remplacement de Display() par ToString(). Implémentation de SchoolMember pour éviter les duplicats.

---

### 10. Receptionist
Smells: Magic Numbers, Missing Validation, Single Responsibility.
Avant : Le salaire par défaut était codé en dur, le texte de plainte pouvait être vide, et on utilisait Display() au lieu de la méthode ToString(). D’ailleurs, la méthode était une longue chaîne, et la façon dont les valeurs étaient appelées augmentait les risques d’erreurs. Sans oublier la classe Complaint qui se trouvait dans le même fichier.
Après : Le salaire par défaut est une constante, on valide le texte de plainte et on utilise ToString().

---

### 11. SchoolEmployee (classe de base)
Smells: Duplicated Code, Inconsistent Error Handling, State non encapsulé.
Avant: Chaque employé dupliquait la logique de paie/solde avec des vérifications différentes.
Après: La logique commune est factorisée dans SchoolEmployee, le contrat IPayroll est clair, PayAsync() vérifie les invariants et expose GetBalance() / ResetBalance(int). Toutes les valeurs sont vérifiées dans la classe.

---

### 12. Address
Smells : Primitive Obsession.
Avant : L’adresse était un string libre, sans structure ni règles.
Après : Address est un objet (rue, ville, code postal) avec validations et un ToString() propre.


## Auteurs
Projet développé dans le cadre du cours **Maintenance Logicielle**.
