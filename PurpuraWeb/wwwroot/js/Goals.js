function goalCardContainerListener() {
    let $goalCardContainer = document.getElementById("goal-card-container");
    let $goalCards = $(".goal-cards");

    let resizeObserver = new ResizeObserver(obs => {
        let width = obs[0].borderBoxSize[0].inlineSize;
        $goalCards.each((index, goal) => goal.style.setProperty("margin-left", "", "important"));

        if ((width > 1331 && width <= 1998) && ($goalCards.length % 2 > 0)) {
            $goalCards.last()[0].style.setProperty("margin-left", "21.5rem", "important");
        }
        else if (width >= 1998 && ($goalCards.length % 3 > 0)) {
            let modulus = $goalCards.length % 3;

            if (modulus == 1) {
                $goalCards.last()[0].style.setProperty("margin-left", "42.1rem", "important");
            }
            else if (modulus == 2) {
                $goalCards[$goalCards.length - 2].style.setProperty("margin-left", "21.5rem", "important");
            }
        }
    });

    if ($goalCards.length) {
        resizeObserver.observe($goalCardContainer);
    }
}