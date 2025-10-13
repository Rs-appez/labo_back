_Exercice API backend dotnet_

# Parc d'attraction

Notre client « ChaMonNam » souhaite créer une
application de gestion de parc d'attraction au travers d'une application
web pour commencer qui sera portée sur les devices mobile dans un futur
lointain.

Cette application devrait permettre la gestion des employés et
des tâches qui leurs seront attribuées.

Le parc d'attraction est divisé
en zone. Pour chacune d'entre elle, un thème est déﬁni, nous avons le
thème Terreur, Waterworld et Bisounours mais nous prévoyons déjà de
créer d'autres thèmes.

Une attraction a un nom et est associée à un
thème (zone), nous sommes réputés pour avoir des attractions tout
public.

Nous avons plusieurs types d'employés, qui pourront se connecter
à l'application :

Les chefs de services : qui gèreront leurs employés,
créer et désactiver ainsi que gérer les tâches qui leurs seront
attribuées, en les créant, en les modiﬁants, en les attribuant, en les
validant, en les annulant.

Attention une tache ne peut être attribuée à
un employé désactivé et nous ne pouvons désactiver un employé à qui il
reste des tâches non validées.

Les employés « simples » : pourra voir
les tâches qui leur seront attribuées, indiquer qu'ils auront terminés
une tâche. Cet accomplissement sera validé par le chef de service. Les
réparateurs auront suivi une formation dont on connaitra la date de
qualiﬁcation

Ils pourront aussi demander un changement d'attribution
pour leurs tâches en justiﬁant chaque demande.

Les taches sont
catégorisées et associées à une attraction :

- Entretien de site (ramassage des déchets autour de l'attraction)
- Entretien (vériﬁcation de l'état de l'attraction) Réparation (faite par
- un employé qualiﬁé) Accueil des visiteurs (gérer les visiteurs de
- l'attraction)

Cette gestion de tâches devrait apparaitre sous forme d'agenda aﬁn de
faciliter le travail des chefs de service

L'application devra permettre
la communication en mettant des commentaires aux di érentes tâches.
