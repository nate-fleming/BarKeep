// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Create dynamic ingredient Inputs
let count = 0;
function getDynamicIngredientInput(values) {
    return `<div data-aos="fade-right" class="row"><div class="col-sm-3"><input type="text" name="Ingredients[${count}].Amount" class="form-control" placeholder="Amount"></div > <div class="col-sm-9 input-group"><input type="text" name="Ingredients[${count}].Name" class="form-control" placeholder="Ingredient Name"><div class="input-group-append"><button class="btn btn-success" type="button" onclick="addIngredientInput()" value="Add"><i class="fas fa-plus-circle"></i></button><button class="btn btn-danger" type="button" onclick="removeIngredientInput(this)" value="Remove"><i class="fas fa-minus-circle "></i></button></div></div></div>`;
}

function addIngredientInput() {
    count += 1;
    var div = document.createElement('DIV');
    div.innerHTML = getDynamicIngredientInput("");
    document.getElementById("ingredientsDiv").appendChild(div);
}

function removeIngredientInput(div) {
    count -= 1;
    var x = document.getElementById("ingredientsDiv").lastChild;
    document.getElementById("ingredientsDiv").removeChild(x);
}

function disableCreateBtn() {
    var createBtn = document.getElementById("cocktailCreateBtn");
    var loadingBtn = document.getElementById("loadingBtn");
    var createDiv = document.getElementById("createDiv")
    createBtn.classList.add("hide");
    //loadingBtn.classList.remove("hide");
    var div = document.createElement('DIV');
    div.innerHTML = `<h4 data-aos="fade-right" id="loadingBtn"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span><i class="fas fa-cocktail fa-2x mr-2"></i>Loading...</h4>`
    createDiv.appendChild(div)
}

function disableEditBtn() {
    var editBtn = document.getElementById("cocktailEditBtn");
    var loadingBtn = document.getElementById("loadingBtn");
    var editDiv = document.getElementById("editDiv")
    editBtn.classList.add("hide");
    //loadingBtn.classList.remove("hide");
    var div = document.createElement('DIV');
    div.innerHTML = `<h4 data-aos="fade-right" id="loadingBtn"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span><i class="fas fa-cocktail fa-2x mr-2"></i>Loading...</h4>`
    editDiv.appendChild(div)
}


// Create dynamic instruction inputs
let instructionCount = 0;
function getDynamicInstructionInput(values) {
    return `<div data-aos="fade-right" class="row"><div class="col-sm-1"><label>Step ${instructionCount + 1}</label></div><div class="col-sm-11 input-group"><input hidden type="text" name="Instructions[${instructionCount}].Number" value="${instructionCount + 1}" /><input type="text" name="Instructions[${instructionCount}].Description" class="form-control" placeholder="Instruction"><div class="input-group-append"><button class="btn btn-success" type="button" onclick="addInstructionInput()" value="Add"><i class="fas fa-plus-circle"></i></button><button class="btn btn-danger" type="button" onclick="removeInstructionInput(this)" value="Remove"><i class="fas fa-minus-circle"></i></button></div></div></div>`;
}

function addInstructionInput() {
    instructionCount += 1;
    var div = document.createElement('DIV');
    div.innerHTML = getDynamicInstructionInput("");
    document.getElementById("instructionsDiv").appendChild(div);
}

function removeInstructionInput(div) {
    instructionCount -= 1;
    var x = document.getElementById("instructionsDiv").lastChild;
    document.getElementById("instructionsDiv").removeChild(x);
}