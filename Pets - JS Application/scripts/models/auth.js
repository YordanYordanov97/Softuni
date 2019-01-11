let auth = (() => {
    function saveSession(userInfo) {
        let userAuth = userInfo._kmd.authtoken;
        sessionStorage.setItem('authtoken', userAuth);
        let userId = userInfo._id;
        sessionStorage.setItem('userId', userId);
        let username = userInfo.username;
        sessionStorage.setItem('username', username);
    }

    // user/login
    function login(username, password) {
        let userData = {
            username,
            password
        };

        return requester.post('user', 'login', 'basic', userData);
    }

    // user/register
    function register(username, password,email,avatarUrl) {
        let userData = {
            username,
            password,
            email,
            avatarUrl
        };

        return requester.post('user', '', 'basic', userData);
    }

    // user/logout
    function logout() {
        let logoutData = {
            authtoken: sessionStorage.getItem('authtoken')
        };

        return requester.post('user', '_logout', 'kinvey', logoutData);
    }

    function getUser(userId) {
        return requester.get('user', userId + '/', 'kinvey');
    }

    function deleteUser(userId) {
        return requester.remove('user', userId +'/', 'kinvey');
    }

    function handleError(reason) {
        showError(reason.responseJSON.description);
    }

    function successInfo(message) {
        let infoBox = $('#infoBox');
        infoBox.on('click',disapearButton);
        let span=$('#infoBox span')[0];
        span.textContent=message;
        infoBox.show();
        setTimeout(() => infoBox.fadeOut(), 3000);
    }
    function disapearButton(){
        $('#loadingBox').css('display','none');
    }

    function showError(message) {
        let errorBox = $('#errorBox');
        let span=$('#errorBox span')[0];
        span.textContent=message;
        $('#errorBox').css('display','block');
        errorBox.on('click',disapearButton);
    }

    function loadInfo() {
        $('#loadingBox').css('display','block');
    }

    return {
        login,
        register,
        logout,
        getUser,
        deleteUser,
        saveSession,
        successInfo,
        showError,
        loadInfo,
        disapearButton,
        handleError
    }
})()