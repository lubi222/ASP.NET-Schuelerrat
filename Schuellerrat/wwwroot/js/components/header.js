class Header extends HTMLElement {
	constructor() {
		super();
	}

	connectedCallback() {
		this.innerHTML = `
        <div>
        <div class="header-inner-pages">
            <div class="top">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-information">
                                <p>Welcome to Educate HTML Template</p>
                            </div>
                            <div class="right-bar">
                                <ul class="flat-information">
                                    <li class="phone">
                                        <a
                                            href="+61383766284"
                                            title="Phone number"
                                            >9999 - 9622 26602</a
                                        >
                                    </li>
                                    <li class="email">
                                        <a
                                            href="mailto:AlitStudios@gmail.com"
                                            title="Email address"
                                        >
                                            info@educate.com</a
                                        >
                                    </li>
                                </ul>
                                <ul class="flat-socials">
                                    <li class="facebook">
                                        <a href="#">
                                            <i class="fa fa-facebook"></i>
                                        </a>
                                    </li>
                                    <li class="twitter">
                                        <a href="#">
                                            <i class="fa fa-twitter"></i>
                                        </a>
                                    </li>
                                    <li class="linkedin">
                                        <a href="#">
                                            <i class="fa fa-linkedin"></i>
                                        </a>
                                    </li>
                                    <li class="youtube">
                                        <a href="#">
                                            <i
                                                class="fa fa-youtube-play"
                                            ></i>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.header-inner-pages -->

        <!-- Header -->
        <header id="header" class="header clearfix">
            <div class="container">
                <div class="header-wrap clearfix">
                    <div id="logo" class="logo">
                        <a href="index.html" rel="home">
                            <img src="images/logo.png" alt="image" />
                        </a>
                    </div>
                    <!-- /.logo -->
                    <div class="nav-wrap">
                        <div class="btn-menu">
                            <span></span>
                        </div>
                        <!-- //mobile menu button -->
                        <nav id="mainnav" class="mainnav">
                            <ul class="menu">
                                <li class="home">
                                    <a href="index.html">Home</a>
                                </li>
                                <li class="has-sub">
                                    <a href="courses-grid.html">Courses</a>
                                    <ul class="submenu">
                                        <li>
                                            <a href="courses-grid.html"
                                                >Courses grid</a
                                            >
                                        </li>
                                        <li>
                                            <a
                                                href="courses-grid-sidebar.html"
                                                >Courses grid sidebar</a
                                            >
                                        </li>
                                        <li>
                                            <a
                                                href="courses-list-sidebar.html"
                                                >Courses list sidebar</a
                                            >
                                        </li>
                                        <li>
                                            <a href="courses-single.html"
                                                >Courses single</a
                                            >
                                        </li>
                                    </ul>
                                    <!-- /.submenu -->
                                </li>

                                <li><a href="about-us.html">About</a></li>

                                <li><a href="team.html">Team</a></li>

                                <li>
                                    <a href="blog.html">Blog</a>
                                    <ul class="submenu">
                                        <li>
                                            <a href="blog.html">Blog</a>
                                        </li>
                                        <li>
                                            <a href="blog-single.html"
                                                >Blog single</a
                                            >
                                        </li>
                                    </ul>
                                    <!-- /.submenu -->
                                </li>

                                <li><a href="contact.html">Contact</a></li>
                            </ul>
                            <!-- /.menu -->
                        </nav>
                        <!-- /.mainnav -->
                    </div>
                    <!-- /.nav-wrap -->

                    <div id="s" class="show-search">
                        <a href="#"><i class="fa fa-search"></i></a>
                    </div>
                    <!-- /.show-search -->

                    <div class="submenu top-search">
                        <div class="widget widget_search">
                            <form class="search-form">
                                <input
                                    type="search"
                                    class="search-field"
                                    placeholder="Search …"
                                />
                                <input
                                    type="submit"
                                    class="search-submit"
                                />
                            </form>
                        </div>
                    </div>
                </div>
                <!-- /.header-inner -->
            </div>
        </header>
        <!-- /.header -->
        `;
	}
}

customElements.define("header-component", Header);
