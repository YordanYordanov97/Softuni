let petsService = (() => {
    function listAllPets() {
        // Request teams from db
        return requester.get('appdata', 'pets' + '?query={}&sort={"likes": -1}', 'kinvey');
    }

    function loadPetDetails(petId) {
        return requester.get('appdata', 'pets/' + petId, 'kinvey');
    }

    function loadMyPets(userId) {
        return requester.get('appdata', 'pets/' + `?query={"_acl.creator":"${userId}"}`, 'kinvey');
    }

    function editPet(petId,name,description,imageURL,category,likes) {
        let petData = {
            name:name,
            description: description,
            imageURL: imageURL,
            category:category,
            likes:likes
        };

        return requester.update('appdata', 'pets/' + petId, 'kinvey', petData);
    }

    function createPet(name,description,imageURL,category,likes) {
        let petData = {
            name:name,
            description: description,
            imageURL: imageURL,
            category:category,
            likes:likes
        };

        return requester.post('appdata', 'pets', 'kinvey', petData);
    }

    function deletePet(petId){
        return requester.remove('appdata', 'pets/' + petId, 'kinvey');

    }


    return {
        listAllPets,
        loadPetDetails,
        loadMyPets,
        editPet,
        createPet,
        deletePet
    }
})();