document.getElementById('rulesDropdown').addEventListener('change', function () {
    var ruleId = this.value;
    var ruleDescriptionDiv = document.getElementById('ruleDescription');

    if (ruleId) {
        fetch(`/Home/GetRuleDescriptionById?id=${ruleId}`)
            .then(response => response.text())
            .then(data => {
                ruleDescriptionDiv.innerHTML = data;
            })
            .catch(error => console.error('Error fetching rule description:', error));
    } else {
        ruleDescriptionDiv.textContent = 'Select a rule...';
    }
});
