# Bienvenue à **SchoolManagerApp**

## Comment exécuter l'application
1. Ouvre un **terminal**.
2. **Navigue** jusqu'au dossier du projet avec la commande:
   ```bash
   cd path/to/SchoolManagerApp
   ```
4. **Exécute** la commande suivante pour lancer l'application:
   ```bash
   npm run dev
   ```

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

## Auteurs
Projet développé dans le cadre du cours **Maintenance Logicielle**.
