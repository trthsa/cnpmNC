const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);
// ======================================= Thư viện Validate =================================== //
function Validator(options){

    function getParent(element, selector){
        while(element.parentElement){
            if(element.parentElement.matches(selector)){
                return  element.parentElement;
            }
            element = element.parentElement
        }
    }

    var selectorRules = {};

    var formElement = document.querySelector(options.form);
    function Validate(rule,inputElement){
        var errorElement = getParent(inputElement,options.formGroupSelector).querySelector(options.selector);
        var errorMessage
        //Lấy ra các rule của selector
        var rules = selectorRules[rule.selector]
        // Lập qua từng rule và kiểm tra
        // Nếu có lỗi dừng việc kiểm tra
        for( var i = 0; i < rules.length;i++){
            switch(inputElement.type){
                case 'radio':
                case 'checkbox':
                    errorMessage = rules[i](formElement.querySelector(rule.selector + ':checked'));
                    break;
                default:
                    errorMessage = rules[i](inputElement.value);
            }
            
            if(errorMessage) break;
        }
        //Logic cũ
        if(errorMessage){
            errorElement.innerHTML = errorMessage;
            getParent(inputElement,options.formGroupSelector).classList.add('invalid')
        }
        else{
            errorElement.innerHTML = '',
            getParent(inputElement,options.formGroupSelector).classList.remove('invalid')
        }
        return !errorMessage;                         
    }
    //
    if(formElement){

        formElement.onsubmit = function(e){

            var isFormValid = true;

            e.preventDefault();
            options.rules.forEach(function(rule){
                var inputElement = formElement.querySelector(rule.selector);
                var isValid = Validate(rule,inputElement);
                if(!isValid){
                    isFormValid = false;
                }
            });
           
            if(isFormValid){
                if(typeof options.onSubmit === 'function'){
                    var enableInputs = formElement.querySelectorAll('[name]:not([disabled])');
                    var formValues = Array.from(enableInputs).reduce(function(values, input){
                        switch(input.type){
                            case 'checkbox':
                                if(!input.matches(':checked')){
                                    values[input.name] = '';
                                    return values;
                                } 
                                if(!Array.isArray(values[input.name])){
                                    values[input.name] = [];
                                }
                                values[input.name].push(input.value);                              
                                break;
                            case 'radio':
                                values[input.name] = formElement.querySelector('input[name]:checked').value
                                break;
                            case 'file':
                                values[input.name] = input.files;
                                break;
                            default:
                                values[input.name] = input.value
                                
                        }
                        return values;
                    },{})
                    options.onSubmit(formValues)
                }
            }
        }



        //Lặp qua mồi rules và xử lý ( lắng nghe sự kiện blur, input ...)
        options.rules.forEach(function(rule){

            //Lưu lại các rules cho input
            if(Array.isArray(selectorRules[rule.selector])){
                selectorRules[rule.selector].push(rule.test)
            }
            else{
                selectorRules[rule.selector] = [rule.test];
            }
            
            var inputElements = formElement.querySelectorAll(rule.selector);
            Array.from(inputElements).forEach(function(inputElement){{
                if(inputElement){
                    //Xử lý blur khỏi input nếu lỗi thì hàm validate chạy báo đỏ
                    inputElement.onblur = function(){
                        Validate(rule,inputElement) 
                    }
                    //Xử lý mỗi khi người dùng nhập vào input thì sẽ xóa đỏ
                    inputElement.oninput = function(){
                        var errorElement = getParent(inputElement,options.formGroupSelector).querySelector(options.selector);
                        errorElement.innerHTML = '',
                        getParent(inputElement,options.formGroupSelector).classList.remove('invalid')
                    }
                }
            }})            
        })
    }
}

Validator.isRequired = function(selector, message){
    return {
        selector,
        test: function(value){
            return value ? undefined : message || 'Mời nhập trường này'
        }
    }
}
Validator.isEmail = function(selector){
    return {
        selector,
        test: function(value){
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return emailRegex.test(value)? undefined : 'Email không hợp lệ'
        }
    }
}
Validator.isPassword = function(selector){
    return {
        selector,
        test: function(value){
            const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
            return passwordRegex.test(value) ? undefined : 'Mật khẩu không hợp lệ'
        }
    }
}
Validator.isConfirmPassword = function(selector, getConfirmValue, message){
    return {
        selector,
        test: function(value){
            return value === getConfirmValue() ? undefined : message || 'Giá trị nhập chưa đúng'
        }
    }
}
// ======================================= End Thư viện Validate =================================== //4


// Chuyển trang đăng nhập / đăng ký
const formHeadingChooses = $$('.form_heading--choose');
const forms = $$('.form');
const descs = $$('.desc')
const buttonSubmit = $$('.form-submit');
console.log(buttonSubmit);

formHeadingChooses.forEach(function(formHeadingChoose,index){
    var form = forms[index];
    var desc = descs[index];
    formHeadingChoose.onclick = function(){
        // Chuyển desc
        $('.desc.active').classList.remove('active');
        desc.classList.add('active');
        // Chuyển màu color 
        $('.form_heading--choose.color').classList.remove('color');
        this.classList.add('color');
        // Chuyển trang
        $('form.active').classList.remove("active");
        form.classList.add("active");
        
    }

})
