# TP214E
Daniel Parent 1233274 et Benjamin Belzile 1935290

#Correction du bogue
La page de commandes n'ouvrait pas pusqu'elle héritait de la classe Window de System.Windows.
Cette classe n'implemente pas la méthode navigate utilisée pour naviguer au travers des pages.

Nous avons tenté de changer le parent de PageCommandes sans succès. L'IDE ramenait le parent Window dans une des déclaration partielle de la classe.
Nous avons donc completement supprimé la "Window" PageCommandes pour ensuite créer un "Page" PageCommades. CQFD

