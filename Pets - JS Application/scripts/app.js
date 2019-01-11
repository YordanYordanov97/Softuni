$(() => {
    const app = Sammy('#container', function () {
        this.use('Handlebars', 'hbs');
        this.get('index.html',displayHome);
        this.get('#/home',displayHome);

        this.get('#/register',function (ctx) {
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
                registerForm:'./templates/register/registerForm.hbs'
            }).then(function(context) {
                this.partial('./templates/register/registerPage.hbs');
            });
        })
        this.post('#/register',function (ctx) {
            let username=ctx.params.username;
            let password=ctx.params.password;
            let email=ctx.params.email;
            let avatarUrl=ctx.params.avatarUrl;
            let validatorResult=validator();
            if(validatorResult!=='success'){
                auth.showError(validatorResult)
            }else{
                    auth.loadInfo();
                    auth.register(username,password,email,avatarUrl)
                        .then(function (userInfo) {
                            auth.disapearButton();
                            auth.saveSession(userInfo);
                            auth.successInfo('User registration successful.');
                            ctx.redirect('#/home');
                        })
                        .catch(auth.handleError);
            }

        });
        this.get('#/login',function (ctx) {
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            ctx.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
                loginForm:'./templates/login/loginForm.hbs'
            }).then(function() {
                this.partial('./templates/login/loginPage.hbs');
            });
        });
        this.post('#/login',function (ctx) {
            let username=ctx.params.username;
            let password=ctx.params.password;
            let validatorResult=validator();
            if(validatorResult!=='success'){
                auth.showError(validatorResult)
            }else{
                auth.login(username,password)
                    .then(function (userInfo) {
                        auth.saveSession(userInfo);
                        auth.successInfo('Login successful.');
                        $("#username").val('');
                        $("#password").val('');
                        ctx.redirect('#/home');
                    })
                    .catch(auth.handleError);
            }
        })
        this.get('#/logout',function (ctx){
            auth.logout()
                .then(function () {
                    sessionStorage.clear();
                    auth.successInfo('Logout successful.');
                    ctx.redirect('#/login');
                }).catch(auth.handleError)
        })
        this.get('#/all',function (ctx){
            displayHome();
        })
        this.get('#/cats',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            if(isAuthorized()){
                auth.loadInfo();
                petsService.listAllPets()
                    .then(function (pets) {
                        auth.disapearButton();
                        let petsSelected=[];
                        for(let pet of pets){
                            if(pet.category==='Cat' && pet._acl.creator!==sessionStorage.getItem('userId')){
                                petsSelected.push(pet);
                            }
                        }
                        ctx.otherPets=petsSelected;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                            otherPet:'./templates/home/otherPet.hbs',
                        }).then(function(context) {
                            this.partial('./templates/home/home.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        })
        this.get('#/dogs',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            if(isAuthorized()){
                auth.loadInfo();
                petsService.listAllPets()
                    .then(function (pets) {
                        auth.disapearButton();
                        let petsSelected=[];
                        for(let pet of pets){
                            if(pet.category==='Dog' && pet._acl.creator!==sessionStorage.getItem('userId')){
                                petsSelected.push(pet);
                            }
                        }
                        ctx.otherPets=petsSelected;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                            otherPet:'./templates/home/otherPet.hbs',
                        }).then(function(context) {
                            this.partial('./templates/home/home.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        })
        this.get('#/parrots',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            if(isAuthorized()){
                auth.loadInfo();
                petsService.listAllPets()
                    .then(function (pets) {
                        auth.disapearButton();
                        let petsSelected=[];
                        for(let pet of pets){
                            if(pet.category==='Parrot' && pet._acl.creator!==sessionStorage.getItem('userId')){
                                petsSelected.push(pet);
                            }
                        }
                        ctx.otherPets=petsSelected;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                            otherPet:'./templates/home/otherPet.hbs',
                        }).then(function(context) {
                            this.partial('./templates/home/home.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        })
        this.get('#/reptiles',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            if(isAuthorized()){
                auth.loadInfo();
                petsService.listAllPets()
                    .then(function (pets) {
                        auth.disapearButton();
                        let petsSelected=[];
                        for(let pet of pets){
                            if(pet.category==='Reptile' && pet._acl.creator!==sessionStorage.getItem('userId')){
                                petsSelected.push(pet);
                            }
                        }
                        ctx.otherPets=petsSelected;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                            otherPet:'./templates/home/otherPet.hbs',
                        }).then(function(context) {
                            this.partial('./templates/home/home.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        })
        this.get('#/others',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            if(isAuthorized()){
                auth.loadInfo();
                petsService.listAllPets()
                    .then(function (pets) {
                        auth.disapearButton();
                        let petsSelected=[];
                        for(let pet of pets){
                            if(pet.category==='Other' && pet._acl.creator!==sessionStorage.getItem('userId')){
                                petsSelected.push(pet);
                            }
                        }
                        ctx.otherPets=petsSelected;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                            otherPet:'./templates/home/otherPet.hbs',
                        }).then(function(context) {
                            this.partial('./templates/home/home.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        })
        this.get('#/add',function (ctx) {
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            if(isAuthorized()) {
                this.loadPartials({
                    header: './templates/common/header.hbs',
                    footer: './templates/common/footer.hbs',
                    createForm: './templates/create/createForm.hbs',
                }).then(function (context) {
                    this.partial('./templates/create/createPage.hbs');
                });
            }else{
                ctx.redirect('#/login');
            }
        })
        this.post('#/add',function (ctx) {
            if(isAuthorized()){
                let name=ctx.params.name;
                let description=ctx.params.description;
                let imageURL=ctx.params.imageURL;
                let category=ctx.params.category;
                auth.loadInfo();
                petsService.createPet(name,description,imageURL,category,0)
                    .then(function (memeInfo) {
                        auth.disapearButton();
                        auth.successInfo(`Pet created.`);
                        displayHome(ctx);
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }

        })
        this.get('#/mypets',function (ctx) {
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            if(isAuthorized()){
                auth.loadInfo();
                petsService.loadMyPets(sessionStorage.getItem('userId'))
                    .then(function (myPets) {
                        auth.disapearButton();
                        ctx.myPets=myPets;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                            myPet:'./templates/myPets/myPet.hbs',
                        }).then(function(context) {
                            this.partial('./templates/myPets/catalogPage.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');

            }
        })

        this.get('#/edit/:petId',function (ctx) {
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');

            let petId=ctx.params.petId.substr(1);
            if(isAuthorized()){
                auth.loadInfo();
                petsService.loadPetDetails(petId)
                    .then(function (petInfo){
                        auth.disapearButton();
                        ctx._id=petInfo._id;
                        ctx.name=petInfo.name;
                        ctx.likes=petInfo.likes;
                        ctx.description=petInfo.description;
                        ctx.imageURL=petInfo.imageURL;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                        }).then(function(context) {
                            this.partial('./templates/edit/editPage.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }

        })
        this.post('#/edit/:petId',function (ctx) {
            let petId=ctx.params.petId.substr(1);
           console.log(petId);
            if(isAuthorized()){
                let description=ctx.params.description;
                auth.loadInfo();
                petsService.loadPetDetails(petId)
                    .then(function (petInfo) {
                        petsService.editPet(petId,petInfo.name,description,petInfo.imageURL,petInfo.category,petInfo.likes)
                            .then(function () {
                                auth.disapearButton();
                                auth.successInfo(`Updated successfully!`);
                                ctx.redirect('#/home');
                            }).catch(auth.handleError);
                    }).catch(auth.handleError);

            }else{
                ctx.redirect('#/login');
            }
        })

        this.get('#/delete/:petId',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');

            let petId=ctx.params.petId.substr(1);
            if(isAuthorized()){
                auth.loadInfo();
                petsService.loadPetDetails(petId)
                    .then(function (petInfo){
                        auth.disapearButton();
                        ctx._id=petInfo._id;
                        ctx.name=petInfo.name;
                        ctx.likes=petInfo.likes;
                        ctx.description=petInfo.description;
                        ctx.imageURL=petInfo.imageURL;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                        }).then(function(context) {
                            this.partial('./templates/delete/deletePage.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        });
        this.post('#/delete/:petId',function (ctx){

            let petId=ctx.params.petId.substr(1);
            if(isAuthorized()){
                auth.loadInfo();
                petsService.deletePet(petId)
                    .then(function () {
                        auth.disapearButton();
                        auth.successInfo(`Pet removed successfully!`);
                        ctx.redirect('#/home');
                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        });

        this.get('#/details/:petId',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');

            let petId=ctx.params.petId.substr(1);
            if(isAuthorized()){
                auth.loadInfo();
                petsService.loadPetDetails(petId)
                    .then(function (petInfo) {
                        auth.disapearButton();
                        ctx._id=petInfo._id;
                        ctx.name=petInfo.name;
                        ctx.likes=petInfo.likes;
                        ctx.description=petInfo.description;
                        ctx.imageURL=petInfo.imageURL;

                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                        }).then(function(context) {
                            this.partial('./templates/details/details.hbs');
                        });

                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }


        })
        this.get('#/detailsLikes/:petId',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');

            let petId=ctx.params.petId.substr(1);
            if(isAuthorized()){
                auth.loadInfo();
                petsService.loadPetDetails(petId)
                    .then(function (petInfo) {
                        auth.disapearButton();
                        petsService.editPet(petId,petInfo.name,petInfo.description,
                            petInfo.imageURL,petInfo.category,Number(petInfo.likes)+1)
                            .then(function () {
                                ctx.redirect(`#/details/:${petId}`);
                            }).catch(auth.handleError);

                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        })

        this.get('#/homeLikes/:petId',function (ctx){
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');

            let petId=ctx.params.petId.substr(1);
            if(isAuthorized()){
                auth.loadInfo();
                petsService.loadPetDetails(petId)
                    .then(function (petInfo) {
                        auth.disapearButton();
                        petsService.editPet(petId,petInfo.name,petInfo.description,
                            petInfo.imageURL,petInfo.category,Number(petInfo.likes)+1)
                            .then(function () {
                                ctx.redirect(`#/home`);
                            }).catch(auth.handleError);

                    }).catch(auth.handleError);
            }else{
                ctx.redirect('#/login');
            }
        })

        function validator() {
            let usernameRegex=/^[a-zA-Z0-9]{3,}$/;
            let passwordRegex=/^[a-zA-Z0-9]{6,}$/;
            let usernameVal=$("input[name='username']").val();
            if(!usernameVal.match(usernameRegex)){
                return 'Username must be at least 3 symbols';
            }
            let passwordVal=$("input[name='password']").val();
            if(!passwordVal.match(passwordRegex)){
                return 'Password must be at least 6 symbols';
            }

            return 'success';
        }
        function displayHome(ctx) {
            ctx.loggedIn=sessionStorage.getItem('authtoken') !==null;
            ctx.username=sessionStorage.getItem('username');
            ctx.userId=sessionStorage.getItem('userId');
            if(isAuthorized()){
                auth.loadInfo();
                petsService.listAllPets()
                    .then(function (pets) {
                        auth.disapearButton();
                        let otherPets=[];
                        for(let pet of pets){
                            if(pet._acl.creator!==sessionStorage.getItem('userId')){
                                otherPets.push(pet);
                            }
                        }
                        ctx.otherPets=otherPets;
                        ctx.loadPartials({
                            header: './templates/common/header.hbs',
                            footer: './templates/common/footer.hbs',
                            otherPet:'./templates/home/otherPet.hbs',
                        }).then(function(context) {
                            this.partial('./templates/home/home.hbs');
                        });
                    }).catch(auth.handleError);
            }else{
                ctx.loadPartials({
                    header: './templates/common/header.hbs',
                    footer: './templates/common/footer.hbs',
                }).then(function(context) {
                    this.partial('./templates/home/home.hbs');
                });
            }


        }
        function isAuthorized(){
            if(sessionStorage.getItem('authtoken') !==null){
                return true;
            }else{
                return false;
            }
        }
    });
    app.run();
});