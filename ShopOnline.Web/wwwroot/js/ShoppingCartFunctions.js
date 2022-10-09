const makeUpdateQtyButtonVisible = (id, visible) => {
    const updateQtyButton = document.querySelector(`button[data-item-id='${id}']`);

    if (visible) {
        updateQtyButton.style.display = 'inline-block';
    } else {
        updateQtyButton.style.display = 'none';
    }
}