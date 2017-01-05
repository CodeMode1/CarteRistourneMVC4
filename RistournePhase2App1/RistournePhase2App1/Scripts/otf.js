$(function () {

    //handle form submission: collecter params du form, envoyer au serveur, obtenir les résultats du serveur et les graffer (embdedded) à la page
    var ajaxFormSubmit = function () {
        //reference au form qui va etre soumis
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        //async call au serveur pour envoyer donnes du form (callback function : reponse du serveur dans param data)
        $.ajax(options).done(function (data) {
            //obtenir l'element du form a envoyer les données par son identifiant
            var $target = $($form.attr("data-otf-target"));
            //la liste parsé du serveur dans data : (callback)
            var $newHtml = $(data);
            //remplacer le html de cet element avec les données du serveur
            $target.replaceWith($newHtml);
            //highlighter la nouvelle liste de résultats recues du serveur
            $newHtml.effect("highlight", { color: '#323299' }, 2000);
        });

        //empecher le browser de faire son action par défaut: naviguer ailleurs, aller au serveur par soi-même, aller recherche la page
        return false;

    };

    var submitAutocompleteForm = function (event, ui) {
        //s'assurer de setter la valeur de l'input a la valeur actuelle recherchée (si select est appelé avant et prend l'ancienne valeur sélectionnée)
        var $input = $(this);
        // pour empecher l'autosubmit du form selon la valeur tapé mais bien la valeur sélectionnée dans la liste(obtenir LE choix selectionné)
        $input.val(ui.item.label);

        //soumettre le form auquel appartient le input marqué autoComplete
        var $form = $input.parents("form:first");
        $form.submit();
    };

    var createAutocomplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-otf-autocomplete"),
            //appeler une fonction pour executer la requete des que l'usager clique sur une suggestion.
            select: submitAutocompleteForm
        };

        $input.autocomplete(options);
    };

    var getPage = function () {
        var $a = $(this);

        //serializer le form pour obtenir le input du user et non juste le page number qui est dans le url.
        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        //get request au serveur sur le url du lien a du pagedList : callback (recevoir données du serveur à placer dans la section marquée par data-otf-target)
        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-otf-target");
            $(target).replaceWith(data);
        });
        return false;
    };

    //aller chercher tous les form qui ont la propriété requete ajax set to true, wired au submit qui appelle methode:ajaxFormSubmit au lieu appeler serveur
    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);
    //aller chercher tous les inputs qui sont marqués autocomplete et appeler la fonction createAutocomplete
    $("input[data-otf-autocomplete]").each(createAutocomplete);
    //rerender la portion de la page ou se trouve le paging seulement, pas rerender les liens car on les detruits a chaque fois qu'on rerender le paging
    $(".main-content").on("click", ".pagedList a", getPage);

});