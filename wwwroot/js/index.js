document.getElementById("add").onclick = function () {
    var node = document.createElement("li");
    node.innerText = newcomer.value;
    list.appendChild(node);
};