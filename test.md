        <form action="" method="POST" class="form" id="form-1">
            <!--  -->
            <div class="form_heading">
                <div class="form_heading--choose color">Đăng Nhập</div>
                <div class="form_heading--choose">Đăng ký</div>
            </div>
            <div class="spacer"></div>
            <!-- Các thẻ input -->
            <div class="form-group">
                <label for="fullname" class="form-label">Tên đầy đủ</label>
                <input id="fullname" name="fullname" type="text" placeholder="VD: Nguyễn Văn A" class="form-control">
                <span class="form-message"></span>
            </div>              
    
            <div class="form-group">
                <label for="email" class="form-label">Email</label>
                <input id="email" name="email" type="text" placeholder="VD: email@domain.com" class="form-control">
                <span class="form-message"></span>
            </div>
        
            <div class="form-group">
                <label for="password" class="form-label">Mật khẩu</label>
                <input id="password" name="password" type="password" placeholder="Nhập mật khẩu" class="form-control">
                <span class="form-message"></span>
            </div>
        
            <div class="form-group">
                <label for="password_confirmation" class="form-label">Nhập lại mật khẩu</label>
                <input id="password_confirmation" name="password_confirmation" placeholder="Nhập lại mật khẩu" type="password" class="form-control">
                <span class="form-message"></span>
            </div>
            <div class="form-group">
                <label for="address" class="form-label">Địa chỉ</label>
                <input id="address" name="address" placeholder="Nhập địa chỉ" type="text" class="form-control">
                <span class="form-message"></span>
            </div>
        
            <div class="form-group">
                <label for="gender" class="form-label">Giới tính </label>
                <div class="form-group-list">
                <div class="form-group-item">
                    <input name="gender" type="radio" value="male" class="form-control">
                    <p>Nam</p>
                </div>
                <div class="form-group-item">
                    <input name="gender" type="radio" value="femal" class="form-control">
                    <p>Nữ</p>
                </div>
                <div class="form-group-item">
                    <input name="gender" type="radio" value="orther" class="form-control">
                    <p>Khác</p>
                </div>       
                </div>
                <span class="form-message"></span>
            </div>   
            <button class="form-submit">Đăng ký</button>
        </form>
        <!-- Đăng nhập -->
        <form action="" method="POST" class="form" id="form-2">
            <!-- Logo -->
            <div class="form_logo">
                <div class="logo_plane" style="background-image: url('assets/img/maybay.svg');">
                    <div class="logo_plane--wings" style="background-image: url('assets/img/canh.svg');"></div>
                </div>
            </div>
            <p class="desc">
                <span>Chào Mừng</span><br>
                Đăng nhập vào tài khoản của quý khách. Dùng tài khoản để làm mọi việc liên quan đến hệ thống của chúng tôi ❤️.
            </p>
            <div class="form_heading">
                <div class="form_heading--choose color">Đăng Nhập</div>
                <div class="form_heading--choose">Đăng ký</div>
            </div>
            <div class="form-group">
                <label for="email" class="form-label">Email</label>
                <input id="email_dn" name="email" type="text" placeholder="VD: email@domain.com" class="form-control">
                <span class="form-message"></span>
              </div>
        
              <div class="form-group">
                <label for="password" class="form-label">Mật khẩu</label>
                <input id="password_dn" name="password" type="password" placeholder="Nhập mật khẩu" class="form-control">
                <span class="form-message"></span>
              </div>
              <button class="form-submit">Đăng Nhập</button>
        </form>v