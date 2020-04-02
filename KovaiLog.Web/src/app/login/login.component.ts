import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from "@angular/router";
import { ToastrService } from 'ngx-toastr';
import { Form, FormGroup, NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  auth2: any;
  public username: string;
  public password: string;
  @ViewChild('loginRef', {static: true }) loginElement: ElementRef;

  constructor(private authenticationService: AuthenticationService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    const currentUser = this.authenticationService.currentUserValue;
    if (currentUser) {
      this.router.navigate(["dashboard"]);
    }
    this.googleSDK();
  }

  login(loginForm: NgForm) {
    if(loginForm.form.valid)
    {
      this.authenticationService.login(this.username, this.password).subscribe(data => {
        console.log(data);
        this.router.navigate(["dashboard"]);
      }, error => {
        this.toastr.error(error);
      });
    }
    else
    {
      this.toastr.info("Please enter the required login details");
    }
    
  }

  prepareLoginButton() {
 
    this.auth2.attachClickHandler(this.loginElement.nativeElement, {},
      (googleUser) => {
 
        let profile = googleUser.getBasicProfile();
        console.log('Token || ' + googleUser.getAuthResponse().id_token);
        console.log('ID: ' + profile.getId());
        console.log('Name: ' + profile.getName());
        console.log('Image URL: ' + profile.getImageUrl());
        console.log('Email: ' + profile.getEmail());
        this.authenticationService.login(profile.getEmail(), "Qwertyuiop123$", true, profile.getName()).subscribe(data => {
          console.log(data);
          window.location.href = "http://localhost:4200/dashboard";
        }, error => {
          this.toastr.error(error);
        });
 
      }, (error) => {
        alert(JSON.stringify(error, undefined, 2));
      });
 
  }

  googleSDK() {
 
    window['googleSDKLoaded'] = () => {
      window['gapi'].load('auth2', () => {
        this.auth2 = window['gapi'].auth2.init({
          client_id: '468039301716-oia3vvfktv1dhhajn0ehus2967lqa2as.apps.googleusercontent.com',
          cookiepolicy: 'single_host_origin',
          scope: 'profile email'
        });
        this.prepareLoginButton();
      });
    }
 
    (function(d, s, id){
      var js, fjs = d.getElementsByTagName(s)[0];
      if (d.getElementById(id)) {return;}
      js = d.createElement(s); js.id = id;
      js.src = "https://apis.google.com/js/platform.js?onload=googleSDKLoaded";
      fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'google-jssdk')); 
  }

}
